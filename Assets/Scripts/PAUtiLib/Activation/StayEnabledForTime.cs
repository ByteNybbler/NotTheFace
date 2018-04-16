// Author(s): Paul Calande
// When the GameObject is enabled, it will only stay enabled for some time.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayEnabledForTime : MonoBehaviour
{
    [SerializeField]
    TimeScale timeScale;
    [SerializeField]
    [Tooltip("How many seconds the GameObject should remain enabled for.")]
    float secondsToStayEnabled;

    // The timer that disables the GameObject when it runs out.
    Timer timerDisable;

    private void OnEnable()
    {
        timerDisable = new Timer(secondsToStayEnabled, TimerFinished);
    }

    private void TimerFinished(float secondsOverflow)
    {
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        timerDisable.Tick(timeScale.DeltaTime());
    }

    // Modify the timer.
    public void SetSecondsToStayEnabled(float secondsToStayEnabled)
    {
        this.secondsToStayEnabled = secondsToStayEnabled;
        if (timerDisable != null)
        {
            timerDisable.SetTargetTime(secondsToStayEnabled);
        }
    }
}