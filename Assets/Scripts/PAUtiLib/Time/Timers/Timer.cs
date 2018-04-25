// Author(s): Paul Calande
// Timer class that can be used to simulate periodic time-based behavior.

public class Timer : ITimer
{
    // Invoked every time the timer finishes a loop.
    // The parameter is how many seconds the timer ran past the target time.
    // Use this parameter in the callback to account for the extra time.
    public delegate void FinishedHandler(float secondsOverflow);
    FinishedHandler Finished;

    // Invoked every time the timer starts from a standstill.
    public delegate void StartedHandler();
    StartedHandler Started;
    
    // Invoked every time the timer stops.
    public delegate void StoppedHandler();
    StoppedHandler Stopped;

    // Invoked every time the timer ticks (but only if the timer is running).
    public delegate void TickedHandler();
    TickedHandler Ticked;

    // How many seconds it takes for the timer to run out of time.
    float secondsTarget;
    // Runner for making the timer start and stop.
    Runner runner;
    // Whether the timer should run on a loop.
    // A looping timer will start over each time it reaches the target time.
    bool loop = true;
    // Whether the timer is cleared every time it is run.
    bool clearOnRun = false;

    // The current number of seconds passed in this period.
    float secondsCurrent = 0.0f;

    // Constructor.
    public Timer(float seconds, FinishedHandler FinishedCallback = null, bool loop = true,
        bool clearOnRun = false, StartedHandler StartedCallback = null,
        StoppedHandler StoppedCallback = null, TickedHandler TickedCallback = null)
    {
        this.secondsTarget = seconds;
        this.Finished = FinishedCallback;
        this.loop = loop;
        this.clearOnRun = clearOnRun;
        this.Started = StartedCallback;
        this.Stopped = StoppedCallback;
        this.Ticked = TickedCallback;
        runner = new Runner(OnStarted, OnStopped);
    }

    // Increase the time passed for this timer by the given amount.
    public void Tick(float deltaTime)
    {
        // If the timer isn't running, this method does nothing.
        if (!runner.IsRunning())
        {
            return;
        }
        // Increase the time passed.
        secondsCurrent += deltaTime;
        // Notify any subscribers that the timer has successfully ticked.
        OnTicked();
        // Check if the timer has finished (i.e. reached its target time).
        // Timers with a very short target time may finish multiple times per
        // update step, so we need to use a while loop to account for multiple
        // timer completions per update step.
        while (secondsCurrent >= secondsTarget)
        {
            // Decrement the time passed to prepare for another loop of this block.
            // The result of this operation is also used to get how many seconds
            // the timer ran past its target time.
            secondsCurrent -= secondsTarget;
            float secondsOverflow = secondsCurrent;
            // If the timer doesn't loop, just stop the timer altogether.
            if (!loop)
            {
                Clear();
                Stop();
            }
            // Invoke the timer finished callback.
            // Pass the "seconds overflow" value we just calculated as well.
            OnFinished(secondsOverflow);
        }
    }

    // Starts or resumes the timer.
    // Returns true if the timer wasn't running when this method was called.
    // The secondsOverflow parameter should be used when a different timer
    // finishing causes this timer to be run.
    public bool Run(float secondsOverflow = 0.0f)
    {
        if (clearOnRun)
        {
            Clear();
        }
        bool startedNow = runner.Run();
        if (startedNow)
        {
            secondsCurrent += secondsOverflow;
        }
        return startedNow;
    }

    // Pauses the timer.
    public bool Stop()
    {
        return runner.Stop();
    }

    // Resets the timer.
    public void Clear()
    {
        secondsCurrent = 0.0f;
    }

    // Returns true if the timer is running.
    public bool IsRunning()
    {
        return runner.IsRunning();
    }

    // Change the target time on the timer.
    public void SetTargetTime(float seconds)
    {
        secondsTarget = seconds;
    }

    // Get the target time on the timer.
    public float GetTargetTime()
    {
        return secondsTarget;
    }

    // Get the current time on the timer.
    public float GetCurrentTime()
    {
        return secondsCurrent;
    }

    // Returns how close (in percent) the timer is to reaching the target time.
    // 1.0 (100%) means the timer has reached the target time.
    public float GetPercentFinished()
    {
        return secondsCurrent / secondsTarget;
    }

    // Change the timer's finished callback function.
    public void SetFinishedCallback(FinishedHandler Callback)
    {
        Finished = Callback;
    }

    // Change the timer's started callback function.
    public void SetStartedCallback(StartedHandler Callback)
    {
        Started = Callback;
    }

    // Change the timer's stopped callback function.
    public void SetStoppedCallback(StoppedHandler Callback)
    {
        Stopped = Callback;
    }

    private void OnFinished(float secondsOverflow)
    {
        if (Finished != null)
        {
            Finished(secondsOverflow);
        }
    }

    private void OnStarted()
    {
        if (Started != null)
        {
            Started();
        }
    }

    private void OnStopped()
    {
        if (Stopped != null)
        {
            Stopped();
        }
    }

    private void OnTicked()
    {
        if (Ticked != null)
        {
            Ticked();
        }
    }
}