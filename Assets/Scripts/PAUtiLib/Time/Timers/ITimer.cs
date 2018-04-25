// Author(s): Paul Calande
// Interface for timers.

public interface ITimer
{
    void Tick(float deltaTime);
    bool Run(float secondsOverflow);
    bool Stop();
    void Clear();
    bool IsRunning();
    void SetTargetTime(float seconds);
    float GetTargetTime();
    float GetCurrentTime();
    float GetPercentFinished();
}