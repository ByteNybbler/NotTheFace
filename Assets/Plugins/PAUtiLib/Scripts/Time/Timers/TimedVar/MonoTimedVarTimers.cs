// Author(s): Paul Calande
// MonoBehaviour wrapper for a TimedVarTimer.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoTimedVarTimers<T> : MonoBehaviour
{
    // Invoked when a PeriodicVar's time runs out.
    public event TimedVarTimers<T>.TimerFinishedHandler TimerFinished;

    [SerializeField]
    TimeScale timeScale;

    // The wrapped timers.
    TimedVarTimers<T> timers;

    private void Awake()
    {
        timers = new TimedVarTimers<T>(OnTimerFinished);
    }

    public void Add(TimedVar<T> var)
    {
        timers.Add(var);
    }

    public void Remove(TimedVar<T> var)
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