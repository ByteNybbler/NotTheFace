// Author(s): Paul Calande
// Timer class to be used for simulating periodic time-based behavior.

public class Timer
{
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
    public Timer(float seconds, bool running = true, bool loop = true)
    {
        this.secondsTarget = seconds;
        this.running = running;
        this.loop = loop;
    }

    // Check if the time has run out, and if not, increment the time passed by deltaTime.
    public bool TimeUp(float deltaTime)
    {
        // If the timer isn't running, always return false.
        if (!running)
        {
            return false;
        }
        // If the timer is finished...
        if (secondsCurrent >= secondsTarget)
        {
            // Behave differently based on whether the timer loops or not.
            if (loop)
            {
                // Short looping timers may fire multiple times per update step.
                secondsCurrent -= secondsTarget;
            }
            else
            {
                // If the timer doesn't loop, stop the timer altogether.
                secondsCurrent = 0.0f;
                running = false;
            }
            return true;
        }
        // The timer isn't finished, so increment the time.
        secondsCurrent += deltaTime;
        return false;
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