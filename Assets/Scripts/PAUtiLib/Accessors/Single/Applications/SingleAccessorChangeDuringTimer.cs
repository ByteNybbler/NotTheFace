// Author(s): Paul Calande
// Changes an accessor's value based on whether a timer is running.

using UnityEngine;

public class SingleAccessorChangeDuringTimer<TValue, TAccessor> : MonoBehaviour
    where TAccessor : SingleAccessor<TValue>
{
    [SerializeField]
    [Tooltip("The new value is assigned while this timer is running.")]
    MonoTimer timer;
    [SerializeField]
    [Tooltip("The component to use to modify the value.")]
    TAccessor accessor;
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