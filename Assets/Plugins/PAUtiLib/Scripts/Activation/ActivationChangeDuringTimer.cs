// Author(s): Paul Calande
// Changes an object's activation state based on whether a timer is running.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationChangeDuringTimer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The new state is assigned while this timer is running.")]
    MonoTimer timer;
    [SerializeField]
    [Tooltip("The GameObject to change the active state of.")]
    GameObject toChange;
    [SerializeField]
    [Tooltip("The new active state to assign. True is enabled, false is disabled.")]
    bool newState;

    // The runner that actually changes the value.
    RunnerChangeValue<bool> runner;

    private void Awake()
    {
        runner = new RunnerChangeValue<bool>(newState, toChange.SetActive, GetActive);
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

    private bool GetActive()
    {
        return toChange.activeSelf;
    }
}