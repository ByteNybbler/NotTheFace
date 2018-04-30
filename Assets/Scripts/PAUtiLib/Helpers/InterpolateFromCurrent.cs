// Author(s): Paul Calande
// Interpolates from the current value to a target value.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterpolateFromCurrent<TValue>
{
    // The function to use for interpolation.
    public delegate TValue InterpolateHandler(TValue start, TValue end, float time);
    InterpolateHandler InterpolateFunction;
    // The callback to use for setting the interpolated value.
    public delegate void SetHandler(TValue value);
    SetHandler Set;
    // The callback to use for getting the current value to interpolate.
    public delegate TValue GetHandler();
    GetHandler Get;

    // The timer to use for the interpolation.
    ITimer timer;
    // The start value for the interpolation.
    TValue start;
    // The target/end value for the interpolation.
    TValue target;
    // Whether the interpolation currently has a target.
    bool hasTarget = false;

    public InterpolateFromCurrent(InterpolateHandler InterpolateFunction, ITimer timer,
        SetHandler SetCallback, GetHandler GetCallback)
    {
        this.InterpolateFunction = InterpolateFunction;
        this.timer = timer;
        timer.SubscribeToTicked(TimerTicked);
        timer.SubscribeToFinished(TimerFinished);
        Set = SetCallback;
        Get = GetCallback;
    }

    public void SetTargetValue(TValue target)
    {
        this.target = target;
        start = Get();
        hasTarget = true;
    }

    public void SetInterpolateFunction(InterpolateHandler InterpolateFunction)
    {
        this.InterpolateFunction = InterpolateFunction;
    }

    public bool HasTarget()
    {
        return hasTarget;
    }

    public bool TimerIsRunning()
    {
        return timer.IsRunning();
    }

    private void TimerTicked()
    {
        if (hasTarget)
        {
            OnSet(InterpolateFunction(start, target, timer.GetPercentFinished()));
        }
    }

    private void TimerFinished(float secondsOverflow)
    {
        if (hasTarget)
        {
            hasTarget = false;
            OnSet(target);
        }
    }

    private void OnSet(TValue value)
    {
        if (Set != null)
        {
            Set(value);
        }
    }
}