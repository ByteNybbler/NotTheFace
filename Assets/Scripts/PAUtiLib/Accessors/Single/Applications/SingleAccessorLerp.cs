// Author(s): Paul Calande
// Lerps an accessor value based on a timer.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingleAccessorLerp<TValue, TAccessor> : MonoBehaviour
    where TAccessor : SingleAccessor<TValue>
{
    [SerializeField]
    [Tooltip("The accessor to assign to while lerping.")]
    TAccessor accessor;
    [SerializeField]
    [Tooltip("The timer to use for lerping.")]
    MonoTimer timer;
    [SerializeField]
    [Tooltip("The target/end value for the lerp.")]
    TValue target;

    // The start value for the lerp.
    TValue start;
    // Whether the lerp currently has a target.
    bool hasTarget = false;

    private void Awake()
    {
        timer.SubscribeToTicked(TimerTicked);
        timer.SubscribeToFinished(TimerFinished);
    }

    private void Start()
    {
        SetTargetValue(target);
    }

    public void SetTargetValue(TValue target)
    {
        this.target = target;
        start = accessor.Get();
        hasTarget = true;
    }

    private void TimerTicked()
    {
        if (hasTarget)
        {
            accessor.Set(Lerp(start, target, timer.GetPercentFinished()));
        }
    }

    private void TimerFinished(float secondsOverflow)
    {
        if (hasTarget)
        {
            hasTarget = false;
            accessor.Set(target);
        }
    }

    // Implement this method in derived classes.
    protected abstract TValue Lerp(TValue start, TValue end, float normalizedTime);
}