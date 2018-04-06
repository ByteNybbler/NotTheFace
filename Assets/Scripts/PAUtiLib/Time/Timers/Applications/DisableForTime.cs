// Author(s): Paul Calande
// Disables a GameObject for an amount of time before enabling it again.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableForTime : MonoBehaviour
{
    public delegate void StateChangedHandler(bool active);
    public event StateChangedHandler StateChanged;

    [SerializeField]
    TimeScale timeScale;
    [SerializeField]
    [Tooltip("The GameObject to disable.")]
    GameObject toDisable;
    [SerializeField]
    [Tooltip("How many seconds to disable the GameObject for before enabling it again.")]
    float secondsToDisable;

    Timer timerDisable;

    private void Start()
    {
        timerDisable = new Timer(secondsToDisable, x => SetActive(true), false, false);
    }

    public void SetSecondsToDisable(float seconds)
    {
        secondsToDisable = seconds;
    }

    public void Run()
    {
        if (!timerDisable.IsRunning())
        {
            SetActive(false);
            timerDisable.Start();
        }
    }

    private void SetActive(bool active)
    {
        toDisable.SetActive(active);
        OnStateChanged(active);
    }

    private void FixedUpdate()
    {
        timerDisable.Tick(timeScale.DeltaTime());
    }

    private void OnStateChanged(bool active)
    {
        if (StateChanged != null)
        {
            StateChanged(active);
        }
    }
}