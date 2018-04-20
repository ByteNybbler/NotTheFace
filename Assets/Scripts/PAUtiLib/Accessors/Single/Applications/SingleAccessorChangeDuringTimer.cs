// Author(s): Paul Calande
// Changes an accessor's value based on whether a MonoTimer is running.

using UnityEngine;

public class SingleAccessorChangeDuringTimer<TValue, TAccessor> : MonoBehaviour
    where TAccessor : SingleAccessor<TValue>
{
    [SerializeField]
    [Tooltip("The component to use to modify the value.")]
    TAccessor accessor;
    [SerializeField]
    [Tooltip("The new value is assigned while this timer is running.")]
    MonoTimer timer;
    [SerializeField]
    [Tooltip("The new value to assign.")]
    TValue value;

    // The runner that actually changes the value.
    RunnerChangeValue<TValue> runner;

    private void Awake()
    {
        runner = new RunnerChangeValue<TValue>(value, accessor.Set, accessor.Get);
        timer.SubscribeToStarted(Run);
        timer.SubscribeToStopped(Stop);
    }

    private void Run()
    {
        runner.Run();
    }

    private void Stop()
    {
        runner.Stop();
    }
}

/*
public class SingleAccessorChangeForTime<TValue, TAccessor> : MonoBehaviour
    where TAccessor : SingleAccessor<TValue>
{
    [SerializeField]
    TimeScale timeScale;
    [SerializeField]
    [Tooltip("The component to use to modify the value.")]
    TAccessor accessor;
    [SerializeField]
    [Tooltip("The value for the accessor to use when the timer starts.")]
    TValue valueChanged;
    [SerializeField]
    [Tooltip("How many seconds should pass before reverting to the old value.")]
    float secondsToChange = 0.1f;

    // The timer that starts when the value is changed.
    Timer timerChanged;
    // The value will revert to this value when the timer runs out.
    TValue valuePrevious;

    private void Start()
    {
        timerChanged = new Timer(secondsToChange, ValueBackToDefault, false);
    }

    // Change the value and start the timer.
    public void Run()
    {
        if (!timerChanged.IsRunning())
        {
            valuePrevious = accessor.Get();
        }
        accessor.Set(valueChanged);
        timerChanged.Clear();
        timerChanged.Run();
    }

    // Stop the timer and revert the color back to normal.
    public void Stop()
    {
        timerChanged.Stop();
        ValueBackToDefault(0.0f);
    }

    // Returns true if the timer is running.
    public bool IsRunning()
    {
        return timerChanged.IsRunning();
    }

    // Callback function for the timer.
    private void ValueBackToDefault(float secondsOverflow)
    {
        accessor.Set(valuePrevious);
    }

    private void FixedUpdate()
    {
        timerChanged.Tick(timeScale.DeltaTime());
    }
}
*/