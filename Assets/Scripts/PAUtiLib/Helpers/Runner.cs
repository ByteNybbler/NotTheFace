// Author(s): Paul Calande
// Class representing a process that can be ran (started) and stopped.

public class Runner
{
    // Invoked every time the runner starts running.
    public delegate void StartedHandler();
    StartedHandler Started;

    // Invoked every time the runner stops running.
    public delegate void StoppedHandler();
    StoppedHandler Stopped;

    // Whether the runner is currently running.
    bool running = false;

    public Runner(StartedHandler StartedCallback = null, StoppedHandler StoppedCallback = null)
    {
        Started = StartedCallback;
        Stopped = StoppedCallback;
    }

    // Returns true if the runner is running.
    public bool IsRunning()
    {
        return running;
    }

    // Runs (starts) the runner.
    // Returns true if the runner wasn't running when this method was called.
    public bool Run()
    {
        bool result = !running;
        if (result)
        {
            running = true;
            OnStarted();
        }
        return result;
    }

    // Makes the runner stop running.
    // Returns true if the runner was running when this method was called.
    public bool Stop()
    {
        bool result = running;
        if (running)
        {
            running = false;
            OnStopped();
        }
        return result;
    }

    public void SetStartedCallback(StartedHandler Callback)
    {
        Started = Callback;
    }

    public void SetStoppedCallback(StoppedHandler Callback)
    {
        Stopped = Callback;
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
}