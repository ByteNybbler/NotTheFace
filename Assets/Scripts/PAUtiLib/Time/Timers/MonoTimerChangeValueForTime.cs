// Author(s): Paul Calande
// MonoBehaviour wrapper for TimerChangeValueForTime.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoTimerChangeValueForTime<TValue> : MonoBehaviour
{
    // Invoked when the timer sets a value.
    event TimerChangeValueForTime<TValue>.ValueSetHandler ValueSet;

    [SerializeField]
    [Tooltip("The time scale to use to progress the timer.")]
    TimeScale timeScale;
    [SerializeField]
    [Tooltip("How many seconds the timer should run for.")]
    float secondsToChange;
    [SerializeField]
    [Tooltip("The value to use when the timer starts.")]
    TValue valueDuringTimer;
    [SerializeField]
    [Tooltip("Whether the timer should be cleared every time it is run.")]
    bool clearOnRun;

    // The timer.
    TimerChangeValueForTime<TValue> timer;

    public void Subscribe(TimerChangeValueForTime<TValue>.ValueSetHandler Callback)
    {
        ValueSet += Callback;
    }

    public void SetValueDuringTimer(TValue value)
    {
        timer.SetValueDuringTimer(value);
    }

    private void Start()
    {
        timer = new TimerChangeValueForTime<TValue>(secondsToChange,
            valueDuringTimer, OnValueSet, clearOnRun);
    }

    public bool Run(TValue valueAfterTimerFinish)
    {
        return timer.Run(valueAfterTimerFinish);
    }

    public void Stop()
    {
        timer.Stop();
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
        secondsToChange = seconds;
        if (timer != null)
        {
            timer.SetTargetTime(seconds);
        }
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

    private void FixedUpdate()
    {
        timer.Tick(timeScale.DeltaTime());
    }
}