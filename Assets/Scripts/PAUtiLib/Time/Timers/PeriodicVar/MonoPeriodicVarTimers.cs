// Author(s): Paul Calande
// MonoBehaviour wrapper for a PeriodicVarTimer.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoPeriodicVarTimers<T> : MonoBehaviour
{
    // Invoked when a PeriodicVar's time runs out.
    public event PeriodicVarTimers<T>.TimerFinishedHandler TimerFinished;

    [SerializeField]
    TimeScale timeScale;

    // The wrapped timers.
    PeriodicVarTimers<T> timers;

    private void Awake()
    {
        timers = new PeriodicVarTimers<T>(OnTimerFinished);
    }

    public void Add(PeriodicVar<T> var)
    {
        timers.Add(var);
    }

    public void Remove(PeriodicVar<T> var)
    {
        timers.Remove(var);
    }

    private void FixedUpdate()
    {
        timers.Tick(timeScale.DeltaTime());
    }

    private void OnTimerFinished(float secondsOverflow, T var)
    {
        if (TimerFinished != null)
        {
            TimerFinished(secondsOverflow, var);
        }
    }
}