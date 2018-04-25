// Author(s): Paul Calande
// Interpolates between one value and another over a given quantity of time.

public class Lerper<T>
{
    // The delegate to use for the actual lerping.
    public delegate T LerpHandler(T a, T b, float t);
    LerpHandler LerpFunction;
    // Invoked when the lerp is finished.
    public delegate void FinishedHandler(float secondsOverflow);
    FinishedHandler Finished;

    // The value at the start of the lerp.
    T start;
    // The value at the end of the lerp.
    T end;
    // How many seconds it takes for the lerp to finish.
    float secondsTarget;
    // The current position in time of the ColorLerper.
    float secondsCurrent = 0.0f;

    public Lerper(LerpHandler LerpFunction, T start, T end, float secondsToLerp,
        FinishedHandler Finished = null)
    {
        this.start = start;
        this.end = end;
        this.secondsTarget = secondsToLerp;
        this.LerpFunction = LerpFunction;
        this.Finished = Finished;
    }

    // Progress by the given amount of time and return the resulting value.
    public T Sample(float deltaTime)
    {
        secondsCurrent += deltaTime;
        if (secondsCurrent >= secondsTarget)
        {
            OnFinished(secondsCurrent - secondsTarget);
        }
        return LerpFunction(start, end, secondsCurrent / secondsTarget);
    }

    private void OnFinished(float secondsOverflow)
    {
        if (Finished != null)
        {
            Finished(secondsOverflow);
        }
    }
}