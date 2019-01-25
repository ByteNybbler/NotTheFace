// Author(s): Paul Calande
// Utility functions for randomness.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilRandom
{
    // Returns a random element from the given collection.
    public static T GetRandomElement<T>(IList<T> collection)
    {
        return collection[Random.Range(0, collection.Count)];
    }

    // Returns the given number of random unique elements from the given collection.
    // In this case, "unique" refers to having a unique index in the given collection.
    public static List<T> GetRandomElementsUnique<T>(IList<T> collection, int count)
    {
        List<int> indices = UniqueIntegersShuffled(count, 0, collection.Count);
        List<T> result = new List<T>(count);
        foreach (int i in indices)
        {
            result.Add(collection[i]);
        }
        return result;
    }

    // Randomly shuffles the given collection in place.
    public static void Shuffle<T>(IList<T> collection)
    {
        // The following is an implementation of the Fisher-Yates shuffle.
        int finalIndex = collection.Count;
        while (finalIndex > 1)
        {
            int chosenIndex = Random.Range(0, finalIndex);
            --finalIndex;
            T value = collection[chosenIndex];
            collection[chosenIndex] = collection[finalIndex];
            collection[finalIndex] = value;
        }
    }

    // Returns an unshuffled collection of unique randomly-chosen integers.
    // The range that the integers are chosen from is [min, max).
    // Do not use this function if the returned integers' order should be randomized too.
    // The order of integers returned by this function is not truly random due to the way
    // that HashSets store data.
    public static HashSet<int> UniqueIntegersUnshuffled(int count, int min, int max)
    {
        HashSet<int> result = new HashSet<int>();
        // Every iteration of the following loop will add a new integer to the collection.
        for (int top = max - count; top < max; ++top)
        {
            // The + 1 makes the top of the Random.Range inclusive.
            // This random range will grow each iteration of the loop.
            if (!result.Add(Random.Range(min, top + 1)))
            {
                // If execution has reached this point, a collision has occurred with
                // an existing integer in the collection.
                // Add the top value, since it could not have been added previously.
                // This top value is equivalent to the inclusive upper end of this
                // loop iteration's Random.Range.
                result.Add(top);
            }
        }
        return result;
    }

    // Returns a shuffled collection of unique randomly-chosen integers.
    // The range that the integers are chosen from is [min, max).
    // The shuffling makes sure that the returned integers' order is also randomized.
    public static List<int> UniqueIntegersShuffled(int count, int min, int max)
    {
        HashSet<int> unshuffled = UniqueIntegersUnshuffled(count, min, max);
        List<int> result = new List<int>(unshuffled);
        Shuffle(result);
        return result;
    }

    // Returns a random position between two points.
    public static Vector3 PositionBetweenTwoPoints(Vector3 point1, Vector3 point2)
    {
        return Vector3.Lerp(point1, point2, Random.Range(0.0f, 1.0f));
    }

    // Returns a random value from the range centered at the given value with the
    // given radius.
    public static float RangeWithCenter(float center, float radius)
    {
        return center + Random.Range(-radius, radius);
    }
    public static int RangeWithCenter(int center, int radius)
    {
        return center + Random.Range(-radius, radius + 1);
    }

    // Randomly returns either true or false based on the given chance.
    public static bool Bool(float chanceOfTrue = 0.5f)
    {
        return Random.Range(0.0f, 1.0f) <= chanceOfTrue;
    }

    // Randomly returns either 1 or -1 based on the given chance.
    public static int Sign(float chanceOfPositive = 0.5f)
    {
        return UtilMath.Sign(Bool(chanceOfPositive));
    }

    // Returns one random character from a string.
    public static char CharacterFromString(string str)
    {
        return str[Random.Range(0, str.Length)];
    }

    // Returns several random characters from a string. Repeats allowed.
    public static string CharactersFromString(string str, int count)
    {
        string result = "";
        for (int i = 0; i < count; ++i)
        {
            result += CharacterFromString(str);
        }
        return result;
    }

    // Spams the given number of characters from the alphabet.
    public static string Spam(int count, bool lowercase = true, bool uppercase = true)
    {
        string str = "";
        if (lowercase)
        {
            str += "abcdefghijklmnopqrstuvwxyz";
        }
        if (uppercase)
        {
            str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        }
        return CharactersFromString(str, count);
    }
}