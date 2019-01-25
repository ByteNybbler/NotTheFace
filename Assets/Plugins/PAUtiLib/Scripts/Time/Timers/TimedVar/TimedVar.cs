// Author(s): Paul Calande
// Data class for a variable that is utilized periodically.

public class TimedVar<T>
{
    // The variable to be wrapped.
    T var;
    // The number of seconds between each utilization of this variable.
    float seconds;

    public TimedVar(T var, float seconds)
    {
        this.var = var;
        this.seconds = seconds;
    }

    public void SetVar(T var)
    {
        this.var = var;
    }

    public void SetSeconds(float seconds)
    {
        this.seconds = seconds;
    }

    public T GetVar()
    {
        return var;
    }

    public float GetSeconds()
    {
        return seconds;
    }
}