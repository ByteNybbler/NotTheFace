// Author(s): Paul Calande
// Component wrapper for a timer that other components can subscribe to.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoTimer : MonoBehaviour
{
    // Invoked when the timer finishes.
    event Timer.FinishedHandler TimerFinished;
    // Invoked when the timer starts from a standstill.
    event Timer.StartedHandler TimerStarted;
    // Invoked when the timer is stopped.
    event Timer.StoppedHandler TimerStopped;

    [SerializeField]
    [Tooltip("The time scale to use to progress the timer.")]
    TimeScale timeScale;
    [SerializeField]
    [Tooltip("The timer to use.")]
    Timer timer;

    // Other components can use these methods to subscribe to timer events.
    public void SubscribeToFinished(Timer.FinishedHandler Callback)
    {
        TimerFinished += Callback;
    }
    public void SubscribeToStarted(Timer.StartedHandler Callback)
    {
        TimerStarted += Callback;
    }
    public void SubscribeToStopped(Timer.StoppedHandler Callback)
    {
        TimerStopped += Callback;
    }

    private void Start()
    {
        timer.SetFinishedCallback(OnTimerFinished);
        timer.SetStartedCallback(OnTimerStarted);
        timer.SetStoppedCallback(OnTimerStopped);
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

    private void OnTimerStarted()
    {
        if (TimerStarted != null)
        {
            TimerStarted();
        }
    }

    private void OnTimerStopped()
    {
        if (TimerStopped != null)
        {
            TimerStopped();
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