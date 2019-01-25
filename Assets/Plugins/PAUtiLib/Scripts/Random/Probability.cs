// Author(s): Paul Calande
// Class for returning one result from a set by random chance.
// T is the result type.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Probability<T> : IDeepCopyable<Probability<T>>
{
    // The dictionary of results and their associated chances.
    // Chances are measured in percent: 0.00 (0%) - 1.00 (100%).
    Dictionary<T, float> chances = new Dictionary<T, float>();
    // The default result.
    // This is the result returned when no other result is chosen.
    T resultDefault;

    public Probability(T resultDefault)
    {
        this.resultDefault = resultDefault;
    }

    public Probability<T> DeepCopy()
    {
        Probability<T> result = new Probability<T>(resultDefault);
        result.chances = new Dictionary<T, float>(chances);
        return result;
    }

    // Check if the chances add up to 1.0 or lower.
    // If they add up to more than 100%, log a warning.
    private void CheckForPercentOverflow()
    {
        float totalChance = 0.0f;
        foreach (KeyValuePair<T, float> pair in chances)
        {
            T result = pair.Key;
            float chance = pair.Value;
            totalChance += chance;
        }
        if (totalChance > 1.0f)
        {
            Debug.LogWarning("Probability percent overflow: " +
                "total probability for all results is " + totalChance);
        }
    }

    // Set the result to be chosen as the default.
    // The default result is the result returned when no other result is chosen.
    public void SetDefaultResult(T val)
    {
        resultDefault = val;
    }

    // Sets the chance of the given result occurring.
    public void SetChance(T result, float chance)
    {
        chances[result] = chance;
        CheckForPercentOverflow();
    }

    // Returns true if the given result is the default result.
    private bool IsDefaultResult(T result)
    {
        return UtilGeneric.IsEqualTo(result, resultDefault);
    }

    // Gets the chance of any result but the default result occurring.
    // Also takes into account any results in the dictionary that match the default result.
    private float GetNonDefaultChance()
    {
        float nonDefaultChance = 0.0f;
        foreach (KeyValuePair<T, float> pair in chances)
        {
            T result = pair.Key;
            if (!IsDefaultResult(result))
            {
                float chance = pair.Value;
                nonDefaultChance += chance;
            }
        }
        return nonDefaultChance;
    }

    // Gets the chance of the default result occurring.
    // This is equivalent to the chance of any of the non-default results NOT occurring.
    private float GetDefaultChance()
    {
        float nonDefaultChance = GetNonDefaultChance();
        float defaultChance = 1.0f - nonDefaultChance;
        // Clamp the returned result to 0 if it's negative.
        defaultChance = Mathf.Max(0.0f, defaultChance);
        return defaultChance;
    }

    // Gets the chance of the given result occurring, taking the default result into account.
    // Returns zero if the result does not have an associated chance.
    public float GetChance(T result)
    {
        float chance = 0.0f;
        if (IsDefaultResult(result))
        {
            chance = GetDefaultChance();
        }
        else
        {
            chances.TryGetValue(result, out chance);
        }
        return chance;
    }

    // Removes the chance of the given result occurring.
    public void RemoveChance(T result)
    {
        chances.Remove(result);
    }

    // Returns a random result. If no result is chosen, the default result is returned,
    // which is possible as long as the combined chances don't add up to 1.
    public T Roll()
    {
        float roll = Random.Range(0.0f, 1.0f);
        float cumulativeChance = 0.0f;
        foreach (KeyValuePair<T, float> pair in chances)
        {
            T result = pair.Key;
            float chance = pair.Value;
            cumulativeChance += chance;
            if (roll <= cumulativeChance)
            {
                return result;
            }
        }
        return resultDefault;
    }
}