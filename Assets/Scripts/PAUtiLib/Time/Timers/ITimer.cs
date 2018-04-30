// Author(s): Paul Calande
// Interface for timers.
// The Tick method is not necessary in timer classes that make use of FixedUpdate.

public interface ITimer
{
    //void Tick(float deltaTime);
    bool Run(float secondsOverflow);
    bool Stop();
    void Clear();
    bool IsRunning();
    void SetTargetTime(float seconds);
    float GetTargetTime();
    float GetCurrentTime();
    float GetPercentFinished();
    void SubscribeToFinished(Timer.FinishedHandler Callback);
    void SubscribeToStarted(Timer.StartedHandler Callback);
    void SubscribeToStopped(Timer.StoppedHandler Callback);
    void SubscribeToTicked(Timer.TickedHandler Callback);
}