// Author(s): Paul Calande
// Component wrapper for a timer that other components can subscribe to.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoTimer : MonoBehaviour
{
    // Invoked when the timer finishes.
    event Timer.FinishedHandler TimerFinished;

    [SerializeField]
    [Tooltip("The time scale to use to progress the timer.")]
    TimeScale timeScale;
    [SerializeField]
    [Tooltip("The timer to use.")]
    Timer timer;

    // Other components can use this to subscribe to the timer.
    public void Subscribe(Timer.FinishedHandler Callback)
    {
        TimerFinished += Callback;
    }

    private void Start()
    {
        timer.SetFinishedCallback(OnTimerFinished);
    }

    private void FixedUpdate()
    {
        timer.Tick(timeScale.DeltaTime());
    }

    private void OnTimerFinished(float secondsOverflow)
    {
        if (TimerFinished != null)
        {
            TimerFinished(secondsOverflow);
        }
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

    public bool Run()
    {
        return timer.Run();
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
}