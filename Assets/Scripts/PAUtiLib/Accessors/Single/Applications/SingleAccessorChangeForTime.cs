// Author(s): Paul Calande
// Changes an accessor's value for a certain amount of time before changing it back.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAccessorChangeForTime<TValue, TAccessor> : MonoBehaviour
    where TAccessor : SingleAccessor<TValue>
{
    [SerializeField]
    TimeScale timeScale;
    [SerializeField]
    [Tooltip("The component to use to modify the value.")]
    TAccessor accessor;
    [SerializeField]
    [Tooltip("The value for the accessor to use when the timer starts.")]
    TValue valueChanged;
    [SerializeField]
    [Tooltip("How many seconds should pass before reverting to the old value.")]
    float secondsToChange = 0.1f;

    // The timer that starts when the value is changed.
    Timer timerChanged;
    // The value will revert to this value when the timer runs out.
    TValue valuePrevious;

    private void Start()
    {
        timerChanged = new Timer(secondsToChange, ValueBackToDefault, false);
    }

    // Change the value and start the timer.
    public void Run()
    {
        if (!timerChanged.IsRunning())
        {
            valuePrevious = accessor.Get();
        }
        accessor.Set(valueChanged);
        timerChanged.Reset();
        timerChanged.Start();
    }

    // Stop the timer and revert the color back to normal.
    public void Stop()
    {
        timerChanged.Stop();
        ValueBackToDefault(0.0f);
    }

    // Returns true if the timer is running.
    public bool IsRunning()
    {
        return timerChanged.IsRunning();
    }

    // Callback function for the timer.
    private void ValueBackToDefault(float secondsOverflow)
    {
        accessor.Set(valuePrevious);
    }

    private void FixedUpdate()
    {
        timerChanged.Tick(timeScale.DeltaTime());
    }
}