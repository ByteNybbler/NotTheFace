// Author(s): Paul Calande
// A runner that changes a value to a given value while it's running.
// When the runner stops, the value will revert back to its previous value.

public class RunnerChangeValue<TValue>
{
    // Callback function used to set the value.
    public delegate void ValueSetHandler(TValue value);
    ValueSetHandler ValueSet;
    // Callback function used to get the value.
    // Utilized to store the previous value.
    public delegate TValue ValueGetHandler();
    ValueGetHandler ValueGet;
    
    // The underlying runner.
    Runner runner;
    // The set callback value to use while running.
    TValue valueRunning;
    // The set callback value to use once the timer stops.
    TValue valueStop;
    
    public RunnerChangeValue(TValue valueRunning,
        ValueSetHandler ValueSetCallback,
        ValueGetHandler ValueGetCallback)
    {
        runner = new Runner(OnStarted, OnStopped);

        ValueSet = ValueSetCallback;
        ValueGet = ValueGetCallback;
        SetValueRunning(valueRunning);
    }

    private void SetValueToRunning()
    {
        OnValueSet(valueRunning);
    }

    private void SetValueToStop()
    {
        OnValueSet(valueStop);
    }

    private void OnStarted()
    {
        valueStop = ValueGet();
        SetValueToRunning();
    }

    private void OnStopped()
    {
        SetValueToStop();
    }

    public void SetValueRunning(TValue valueRunning)
    {
        this.valueRunning = valueRunning;
        if (runner.IsRunning())
        {
            SetValueToRunning();
        }
    }

    public bool IsRunning()
    {
        return runner.IsRunning();
    }

    public bool Run()
    {
        return runner.Run();
    }

    public bool Stop()
    {
        return runner.Stop();
    }

    private void OnValueSet(TValue value)
    {
        if (ValueSet != null)
        {
            ValueSet(value);
        }
    }
}