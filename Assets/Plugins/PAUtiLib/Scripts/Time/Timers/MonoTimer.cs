// Author(s): Paul Calande
// Component wrapper for a timer that other components can subscribe to.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoTimer : MonoBehaviour, ITimer
{
    // Invoked when the timer finishes.
    event Timer.FinishedHandler TimerFinished;
    // Invoked when the timer starts from a standstill.
    event Timer.StartedHandler TimerStarted;
    // Invoked when the timer is stopped.
    event Timer.StoppedHandler TimerStopped;
    // Invoked when the timer ticks.
    event Timer.TickedHandler TimerTicked;

    [SerializeField]
    [Tooltip("The time scale to use to progress the timer.")]
    TimeScale timeScale;
    [SerializeField]
    [Tooltip("The timer's target time.")]
    float seconds;
    [SerializeField]
    [Tooltip("Whether the timer loops.")]
    bool loop;
    [SerializeField]
    [Tooltip("Whether the timer should be cleared every time it is run.")]
    bool clearOnRun;

    // The timer to use.
    Timer timer;

    private void Awake()
    {
        timer = new Timer(seconds, OnTimerFinished, loop, clearOnRun, OnTimerStarted,
            OnTimerStopped, OnTimerTicked);
    }

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
    public void SubscribeToTicked(Timer.TickedHandler Callback)
    {
        TimerTicked += Callback;
    }

    private void FixedUpdate()
    {
        timer.Tick(timeScale.DeltaTime());
    }

    private void OnTimerFinished(float secondsOverflow)
    {
        //Debug.Log("MonoTimer OnTimerFinished");
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

    private void OnTimerTicked()
    {
        if (TimerTicked != null)
        {
            TimerTicked();
        }
    }

    public void SetSecondsTarget(float seconds)
    {
        this.seconds = seconds;
        if (timer != null)
        {
            timer.SetSecondsTarget(seconds);
        }
    }

    public float GetSecondsTarget()
    {
        return seconds;
    }

    public float GetSecondsPassed()
    {
        return timer.GetSecondsPassed();
    }

    public float GetSecondsRemaining()
    {
        return timer.GetSecondsRemaining();
    }

    public float GetPercentFinished()
    {
        return timer.GetPercentFinished();
    }

    public float GetPercentRemaining()
    {
        return timer.GetPercentRemaining();
    }

    public bool Run(float secondsOverflow = 0.0f)
    {
        return timer.Run(secondsOverflow);
    }

    public bool Stop()
    {
        return timer.Stop();
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