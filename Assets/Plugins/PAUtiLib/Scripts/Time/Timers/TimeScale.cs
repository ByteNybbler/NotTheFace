// Author(s): Paul Calande
// Convenient script for scaling time on a per-GameObject basis.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScale : MonoBehaviour
{
    // The data encapsulated by this TimeScale.
    TimeScaleData data;

    private void Awake()
    {
        data = new TimeScaleData();
    }

    // Makes the data reference refer to a different data instance.
    public void SetData(TimeScaleData val)
    {
        data = val;
    }

    // Set the data reference to the data from a different TimeScale.
    public void SetData(TimeScale timeScale)
    {
        SetData(timeScale.data);
    }

    public float DeltaTime()
    {
        return data.DeltaTime();
    }

    public float DeltaTimeNotFixed()
    {
        return data.DeltaTimeNotFixed();
    }

    public void SetTimeScale(float val)
    {
        data.SetTimeScale(val);
    }

    public float GetTimeScale()
    {
        return data.GetTimeScale();
    }

    public void TogglePause()
    {
        data.TogglePause();
    }

    public bool IsPaused()
    {
        return data.IsPaused();
    }

    public bool IsFrozen()
    {
        return data.IsFrozen();
    }
}