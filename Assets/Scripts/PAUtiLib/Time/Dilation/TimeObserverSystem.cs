// Author(s): Paul Calande
// The backbone of a system for implementing time dilation based on a single observer.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeObserverSystem : MonoBehaviour
{
    // Invoked when the observer's time rate is changed.
    public delegate void ObserverTimeRateChangedHandler();
    public event ObserverTimeRateChangedHandler ObserverTimeRateChanged;

    [SerializeField]
    [Tooltip("The observer to use as the frame of reference when perceiving time.")]
    TimeObservable observer;

    // The inverse of the time rate of the observer.
    // Used for calculating the time scales of other objects.
    float observerInverseTimeRate = 1.0f;

    // Called when an item sets a new time rate.
    public void ItemChangedTimeRate(TimeObservable item)
    {
        if (item == observer)
        {
            // Update the inverse time rate.
            observerInverseTimeRate = 1 / item.GetTimeRate();
            // Recalculate the time scales of all non-observer objects.
            OnObserverTimeRateChanged();
        }
    }

    // Returns the observed time scale of a given object.
    public float GetObservedTimeScale(TimeObservable item)
    {
        // The observer's perception of the observer's own time is always constant.
        return item.GetTimeRate() * observerInverseTimeRate;
    }

    private void OnObserverTimeRateChanged()
    {
        if (ObserverTimeRateChanged != null)
        {
            ObserverTimeRateChanged();
        }
    }
}