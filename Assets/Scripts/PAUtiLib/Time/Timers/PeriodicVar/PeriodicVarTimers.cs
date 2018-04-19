// Author(s): Paul Calande
// A collection of timers for utilizing PeriodicVars that wrap a given type T.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicVarTimers<T>
{
    // Callback for when a PeriodicVar's time runs out.
    public delegate void TimerFinishedHandler(float secondsOverflow, T var);
    TimerFinishedHandler TimerFinished;

    // Dictionary that keeps track of the time passed for each PeriodicVar.
    Dictionary<PeriodicVar<T>, Timer> timers = new Dictionary<PeriodicVar<T>, Timer>();

    public PeriodicVarTimers(TimerFinishedHandler callback)
    {
        TimerFinished = callback;
    }

    public void Add(PeriodicVar<T> var)
    {
        Timer timer = new Timer(var.GetSeconds(), (t) => TimerFinished(t, var.GetVar()));
        timer.Start();
        timers.Add(var, timer);
    }

    public void Remove(PeriodicVar<T> var)
    {
        timers.Remove(var);
    }

    public void Tick(float deltaTime)
    {
        Dictionary<PeriodicVar<T>, Timer> iterate =
            new Dictionary<PeriodicVar<T>, Timer>(timers);
        foreach (KeyValuePair<PeriodicVar<T>, Timer> pair in iterate)
        {
            pair.Value.Tick(deltaTime);
        }
    }
}