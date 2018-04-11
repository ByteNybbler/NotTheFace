// Author(s): Paul Calande
// Timer class to be used for simulating periodic time-based behavior.

public class Timer
{
    // Invoked every time the timer finishes a loop.
    // The parameter is how many seconds the timer ran past the target time.
    // Use this parameter in the callback to account for the extra time.
    public delegate void FinishedHandler(float secondsOverflow);
    FinishedHandler finishedCallback;

    // How many seconds it takes for the timer to run out of time.
    float secondsTarget;
    // The current number of seconds passed in this period.
    float secondsCurrent = 0.0f;
    // Whether the timer is currently running.
    bool running;
    // Whether the timer should run on a loop.
    // A looping timer will start over each time it reaches the target time.
    bool loop;

    // Constructor.
    public Timer(float seconds, FinishedHandler finishedCallback = null,
        bool running = true, bool loop = true)
    {
        this.secondsTarget = seconds;
        this.finishedCallback = finishedCallback;
        this.running = running;
        this.loop = loop;
    }

    // Increase the time passed for this timer by the given amount.
    public void Tick(float deltaTime)
    {
        // If the timer isn't running, this method does nothing.
        if (!running)
        {
            return;
        }
        // Increase the time passed.
        secondsCurrent += deltaTime;
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
                secondsCurrent = 0.0f;
                running = false;
            }
            // Invoke the timer finished callback.
            // Pass the "seconds overflow" value we just calculated as well.
            if (finishedCallback != null)
            {
                finishedCallback(secondsOverflow);
            }
        }
    }

    // Change the target time on the timer.
    public void SetTargetTime(float seconds)
    {
        secondsTarget = seconds;
    }

    // Get the current time on the timer.
    public float GetCurrentTime()
    {
        return secondsCurrent;
    }

    // Get the target time on the timer.
    public float GetTargetTime()
    {
        return secondsTarget;
    }

    // Returns how close (in percent) the timer is to reaching the target time.
    // 1.0 (100%) means the timer has reached the target time.
    public float GetPercentFinished()
    {
        return secondsCurrent / secondsTarget;
    }

    // Start or resume the timer.
    public void Start()
    {
        running = true;
    }

    // Pause the timer.
    public void Stop()
    {
        running = false;
    }

    // Reset the timer.
    public void Reset()
    {
        secondsCurrent = 0.0f;
    }

    // Returns true if the timer is running.
    public bool IsRunning()
    {
        return running;
    }
}