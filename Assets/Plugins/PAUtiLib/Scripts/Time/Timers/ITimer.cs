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
    void SetSecondsTarget(float seconds);
    float GetSecondsTarget();
    float GetSecondsPassed();
    float GetSecondsRemaining();
    float GetPercentFinished();
    float GetPercentRemaining();
    void SubscribeToFinished(Timer.FinishedHandler Callback);
    void SubscribeToStarted(Timer.StartedHandler Callback);
    void SubscribeToStopped(Timer.StoppedHandler Callback);
    void SubscribeToTicked(Timer.TickedHandler Callback);
}