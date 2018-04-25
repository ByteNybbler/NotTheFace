// Author(s): Paul Calande
// A collection of factors that get multiplied together to create a single time rate.
// This is useful for making an object susceptible to many different time-modifying objects.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDilationIndexedList : FloatProductIndexedList
{
    [SerializeField]
    [Tooltip("The TimeObservable component to be made susceptible to the time factors.")]
    TimeObservable timeObservable;

    private void Awake()
    {
        SubscribeToProductUpdated(timeObservable.SetTimeRate);
    }
}