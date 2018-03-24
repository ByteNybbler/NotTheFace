// Author(s): Paul Calande
// Script that oscillates the time factor of a time region.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRegionOscillate : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The time region to oscillate the time factor of.")]
    TimeRegion timeRegion;
    [SerializeField]
    TimeScale timeScale;
    [SerializeField]
    [Tooltip("Oscillation magnitude.")]
    float magnitude;
    [SerializeField]
    [Tooltip("Oscillation speed.")]
    float speed;

    Oscillator osc;

    private void Start()
    {
        osc = new Oscillator(magnitude, speed, Mathf.Sin);
    }

    private void FixedUpdate()
    {
        float difference = osc.SampleDelta(timeScale.DeltaTime());
        timeRegion.SetTimeFactor(timeRegion.GetTimeFactor() + difference);
    }
}