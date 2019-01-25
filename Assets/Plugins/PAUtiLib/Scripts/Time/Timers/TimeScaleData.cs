// Author(s): Paul Calande
// Data manipulated by a TimeScale.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleData
{
    // The multiplier for time.
    float timeScale = 1.0f;
    // Whether the time scale is paused.
    // Pausing the time scale effectively sets delta time to zero.
    bool paused = false;
    // Stores the scaled fixed delta time based on the time scale settings.
    // Helps avoid unnecessary repeated calculations.
    float cachedDeltaTime;

    public TimeScaleData()
    {
        CacheDeltaTime();
    }

    // Returns a new delta time that is modified by the time scale settings.
    private float ConvertDeltaTime(float deltaTime)
    {
        if (paused)
        {
            return 0.0f;
        }
        else
        {
            return deltaTime * timeScale;
        }
    }

    // Update the cached delta time value based on the time scale settings.
    private void CacheDeltaTime()
    {
        cachedDeltaTime = ConvertDeltaTime(Time.fixedDeltaTime);
    }

    // Returns the fixed delta time, taking the time scale into account.
    // Returns zero if the time scale is paused.
    // To be used for FixedUpdate's time step.
    public float DeltaTime()
    {
        return cachedDeltaTime;
    }

    // Like the DeltaTime method, but it returns the non-fixed delta time.
    // For use in Update rather than FixedUpdate.
    public float DeltaTimeNotFixed()
    {
        return ConvertDeltaTime(Time.deltaTime);
    }

    public void SetTimeScale(float val)
    {
        timeScale = val;
        CacheDeltaTime();
    }

    public float GetTimeScale()
    {
        return timeScale;
    }

    // Toggles whether the time scale is paused, effectively setting the time scale to zero.
    // This method is intended to be called by pause menus.
    // Do not use this for "time stop" gameplay effects.
    public void TogglePause()
    {
        paused = !paused;
        CacheDeltaTime();
    }

    // Returns true if the time scale is paused.
    public bool IsPaused()
    {
        return paused;
    }

    // Returns true if time is frozen for this time scale.
    // To be used to check whether time has been stopped during gameplay.
    // Will not necessarily return true if the paused variable is true.
    public bool IsFrozen()
    {
        return timeScale == 0.0f;
    }
}