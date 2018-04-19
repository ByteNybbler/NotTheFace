// Author(s): Paul Calande
// Changes a GameObject's active state for an amount of time before reverting it.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationForTime : MonoBehaviour
{
    // Invoked when the target GameObject is either activated or deactivated.
    public delegate void StateChangedHandler(bool active);
    event StateChangedHandler StateChanged;

    [SerializeField]
    TimeScale timeScale;
    [SerializeField]
    [Tooltip("The GameObject to enable and disable.")]
    GameObject target;
    [SerializeField]
    [Tooltip("The state to change the GameObject to. True is enabled, false is disabled.")]
    bool newActiveState;
    [SerializeField]
    [Tooltip("How many seconds to change the GameObject's active state before reverting it.")]
    float secondsToChange;

    // The timer for tracking how long the GameObject should remain in the changed state.
    Timer timerChanged;

    // Subscribe to the StateChanged event.
    public void Subscribe(StateChangedHandler StateChangedCallback)
    {
        StateChanged += StateChangedCallback;
        StateChangedCallback(GetTargetActive());
    }

    public void SetSecondsToChange(float secondsToChange)
    {
        this.secondsToChange = secondsToChange;
    }

    private void Start()
    {
        timerChanged = new Timer(secondsToChange, TimerCallback, false);
    }

    private void TimerCallback(float secondsOverflow)
    {
        SetTargetActive(!newActiveState);
    }

    public void Run()
    {
        if (!timerChanged.IsRunning())
        {
            SetTargetActive(newActiveState);
            timerChanged.SetTargetTime(secondsToChange);
            timerChanged.Start();
        }
    }

    private void SetTargetActive(bool active)
    {
        target.SetActive(active);
        OnStateChanged();
    }

    private bool GetTargetActive()
    {
        return target.activeSelf;
    }

    private void FixedUpdate()
    {
        timerChanged.Tick(timeScale.DeltaTime());
    }

    private void OnStateChanged()
    {
        if (StateChanged != null)
        {
            StateChanged(GetTargetActive());
        }
    }
}