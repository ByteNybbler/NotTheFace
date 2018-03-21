// Author(s): Paul Calande
// A collection of factors that get multiplied together to create a single time rate.
// This is useful for making an object susceptible to many different time-modifying objects.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFactors : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The TimeObservable component to be made susceptible to the time factors.")]
    TimeObservable timeObservable;

    IndexedList<float> factors = new IndexedList<float>();

    // Adjusts the time rate based on the collection of factors.
    private void UpdateTimeRate()
    {
        float product = 1.0f;
        foreach (float fact in factors.GetAllValues())
        {
            product *= fact;
        }
        timeObservable.SetTimeRate(product);
    }

    // Adds a factor to the collection and adjusts the time rate accordingly.
    // Returns the index of the factor.
    public int Add(float factor)
    {
        int index = factors.Add(factor);
        UpdateTimeRate();
        return index;
    }

    // Removes the factor with the given index.
    // Adjusts the time rate accordingly.
    public void Remove(int index)
    {
        factors.Remove(index);
        UpdateTimeRate();
    }

    // Sets the factor with the given index to the given value.
    // Adjusts the time rate accordingly.
    public void Set(int index, float factor)
    {
        factors.Set(index, factor);
        UpdateTimeRate();
    }
}