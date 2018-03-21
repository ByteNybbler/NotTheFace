// Author(s): Paul Calande
// Script component representing the rate at which an observable object moves through time.
// The time scale will be modified based on the perception of the observer.
// This essentially makes it possible to create the effect of time dilation.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeObservable : MonoBehaviour
{
    // Invoked when the time rate is changed.
    public delegate void TimeRateChangedHandler(float newTimeRate);
    public event TimeRateChangedHandler TimeRateChanged;

    [SerializeField]
    [Tooltip("The observer system that calculates this object's time scale.")]
    TimeObserverSystem observerSystem;
    [SerializeField]
    [Tooltip("The time scale to modify.")]
    TimeScale timeScale;

    // The rate at which time passes for this object.
    float timeRate = 1.0f;

    private void Start()
    {
        if (observerSystem != null)
        {
            SubscribeToTimeObserverSystem();
        }
    }

    private void UpdateTimeScale()
    {
        timeScale.SetTimeScale(observerSystem.GetObservedTimeScale(this));
    }

    public void SetTimeRate(float timeRate)
    {
        this.timeRate = timeRate;
        observerSystem.ItemChangedTimeRate(this);
        UpdateTimeScale();
        OnTimeRateChanged(timeRate);
    }

    public float GetTimeRate()
    {
        return timeRate;
    }

    private void SubscribeToTimeObserverSystem()
    {
        // Update the time scale every time the observer's time rate is changed.
        observerSystem.ObserverTimeRateChanged += UpdateTimeScale;
    }

    public void SetTimeObserverSystem(TimeObserverSystem system)
    {
        if (observerSystem != null)
        {
            observerSystem.ObserverTimeRateChanged -= UpdateTimeScale;
        }
        observerSystem = system;
        SubscribeToTimeObserverSystem();
        UpdateTimeScale();
    }

    private void OnTimeRateChanged(float newTimeRate)
    {
        if (TimeRateChanged != null)
        {
            TimeRateChanged(newTimeRate);
        }
    }
}