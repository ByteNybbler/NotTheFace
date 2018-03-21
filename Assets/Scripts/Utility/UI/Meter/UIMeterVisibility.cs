// Author(s): Paul Calande
// Class for controlling UI meter visibility.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMeterVisibility : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The UIMeter to track.")]
    UIMeter meter;
    [SerializeField]
    [Tooltip("Whether the meter should be invisible when it becomes 100% filled.")]
    bool invisibleWhenFull = false;
    [SerializeField]
    [Tooltip("Whether the meter should be invisible when it becomes 0% filled.")]
    bool invisibleWhenEmpty = false;
    [SerializeField]
    [Tooltip("If true, the meter will always be invisible.")]
    bool forcedInvisible = false;

    private void Start()
    {
        meter.PercentChanged += UIMeter_OnPercentChanged;
        CheckMeterVisibility();
    }

    // Makes the meter visible or invisible based on the current percentage.
    private void CheckMeterVisibility()
    {
        if (forcedInvisible)
        {
            SetMeterVisible(false);
            return;
        }
        float percent = meter.GetCurrentPercent();
        if (percent <= 0.0f)
        {
            // The meter is empty.
            if (invisibleWhenEmpty)
            {
                SetMeterVisible(false);
            }
        }
        else if (percent >= 1.0f)
        {
            // The meter is full.
            if (invisibleWhenFull)
            {
                SetMeterVisible(false);
            }
        }
        else
        {
            // The meter is not empty or full.
            SetMeterVisible(true);
        }
    }

    // Turns forced invisibility on or off.
    public void SetForcedInvisible(bool invisible)
    {
        forcedInvisible = invisible;
        CheckMeterVisibility();
    }

    // Makes the meter visible or invisible.
    private void SetMeterVisible(bool visible)
    {
        meter.gameObject.SetActive(visible);
    }

    private void UIMeter_OnPercentChanged(float percentOld, float percentNew)
    {
        CheckMeterVisibility();
    }
}