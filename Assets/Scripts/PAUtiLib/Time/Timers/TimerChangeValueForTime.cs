// Author(s): Paul Calande
// A timer that changes a value for a certain amount of time before changing it back.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerChangeValueForTime<TValue>
{
    // Callback function to use to set the value.
    public delegate void ValueSetHandler(TValue value);
    ValueSetHandler ValueSet;

    // The timer.
    Timer timer;
    // The set callback value to use when the timer starts.
    TValue valueDuringTimer;
    // The set callback value to use when the timer finishes.
    TValue valueAfterTimerFinish;

    public TimerChangeValueForTime(float seconds,
        TValue valueDuringTimer,
        ValueSetHandler ValueSetCallback = null,
        bool clearOnRun = false)
    {
        ValueSet = ValueSetCallback;
        this.valueDuringTimer = valueDuringTimer;
        timer = new Timer(seconds, TimerFinishedCallback, false, clearOnRun);
    }

    public void SetValueDuringTimer(TValue value)
    {
        valueDuringTimer = value;
        if (timer.IsRunning())
        {
            OnValueSet(value);
        }
    }

    // Revert the value to the value corresponding to the finished timer.
    private void RevertValue()
    {
        OnValueSet(valueAfterTimerFinish);
    }

    private void TimerFinishedCallback(float secondsOverflow)
    {
        RevertValue();
    }

    public void Tick(float deltaTime)
    {
        timer.Tick(deltaTime);
    }

    public bool Run(TValue valueAfterTimerFinish)
    {
        bool result = !timer.IsRunning();
        // Only update valueAfterTimerFinish if the timer isn't already running.
        if (timer.Run())
        {
            this.valueAfterTimerFinish = valueAfterTimerFinish;
            OnValueSet(valueDuringTimer);
        }
        return result;
    }

    public void Stop()
    {
        timer.Stop();
        RevertValue();
    }

    public void Clear()
    {
        timer.Clear();
    }

    public bool IsRunning()
    {
        return timer.IsRunning();
    }

    public void SetTargetTime(float seconds)
    {
        timer.SetTargetTime(seconds);
    }

    public float GetTargetTime()
    {
        return timer.GetTargetTime();
    }

    public float GetCurrentTime()
    {
        return timer.GetCurrentTime();
    }

    public float GetPercentFinished()
    {
        return timer.GetPercentFinished();
    }

    private void OnValueSet(TValue value)
    {
        if (ValueSet != null)
        {
            ValueSet(value);
        }
    }
}