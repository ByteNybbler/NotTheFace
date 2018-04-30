// Author(s): Paul Calande
// Interpolates an accessor value based on a timer.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingleAccessorInterpolateFromCurrent<TValue, TAccessor>
    : MonoBehaviour
    where TAccessor : SingleAccessor<TValue>
{
    [SerializeField]
    [Tooltip("The accessor to assign to while interpolating.")]
    TAccessor accessor;
    [SerializeField]
    [Tooltip("The timer to use for interpolating.")]
    MonoTimer timer;
    [SerializeField]
    [Tooltip("The target/end value for the interpolation.")]
    TValue target;

    InterpolateFromCurrent<TValue> interpolator;

    private void Awake()
    {
        interpolator = new InterpolateFromCurrent<TValue>(null, timer,
            accessor.Set, accessor.Get);
    }

    private void Start()
    {
        OverrideThis();
        SetTargetValue(target);
    }

    public void SetTargetValue(TValue target, bool startTimer = false)
    {
        interpolator.SetTargetValue(target);
        if (startTimer)
        {
            timer.Run();
        }
    }

    // Call this method in derived classes.
    public void SetInterpolateFunction(InterpolateFromCurrent<TValue>.InterpolateHandler fn)
    {
        interpolator.SetInterpolateFunction(fn);
    }

    public bool HasTarget()
    {
        return interpolator.HasTarget();
    }

    public bool TimerIsRunning()
    {
        return interpolator.TimerIsRunning();
    }

    // Override this method in derived classes and call SetInterpolateFunction.
    protected abstract void OverrideThis();
}