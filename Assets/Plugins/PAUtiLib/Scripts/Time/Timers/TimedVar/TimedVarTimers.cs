// Author(s): Paul Calande
// A collection of timers for utilizing TimedVars that wrap a given type T.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedVarTimers<T>
{
    // Callback for when a PeriodicVar's time runs out.
    public delegate void TimerFinishedHandler(float secondsOverflow, T var);
    TimerFinishedHandler TimerFinished;

    // Dictionary that keeps track of the time passed for each PeriodicVar.
    Dictionary<TimedVar<T>, Timer> timers = new Dictionary<TimedVar<T>, Timer>();

    public TimedVarTimers(TimerFinishedHandler callback)
    {
        TimerFinished = callback;
    }

    public void Add(TimedVar<T> var)
    {
        Timer timer = new Timer(var.GetSeconds(), (t) => TimerFinished(t, var.GetVar()));
        timer.Run();
        timers.Add(var, timer);
    }

    public void Remove(TimedVar<T> var)
    {
        timers.Remove(var);
    }

    public void Tick(float deltaTime)
    {
        Dictionary<TimedVar<T>, Timer> iterate =
            new Dictionary<TimedVar<T>, Timer>(timers);
        foreach (KeyValuePair<TimedVar<T>, Timer> pair in iterate)
        {
            pair.Value.Tick(deltaTime);
        }
    }
}