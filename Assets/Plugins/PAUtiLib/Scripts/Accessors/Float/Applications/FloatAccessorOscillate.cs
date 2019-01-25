// Author(s): Paul Calande
// Script that oscillates the float of an accessor.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAccessorOscillate : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The accessor to oscillate the value of.")]
    FloatAccessor accessor;
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
        accessor.Set(accessor.Get() + difference);
    }
}