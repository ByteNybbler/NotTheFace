﻿// Author(s): Paul Calande
// A class that tracks oscillations, typically via a trigonometric function.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator
{
    public delegate float WaveformFunction(float f);

    // The multiplier for the magnitude of the oscillation.
    float magnitude;
    // The multiplier for the speed of the oscillation.
    float speed;
    // The function to use to generate the waveform.
    WaveformFunction waveform;

    // The current progress of the oscillation in time.
    float progress = 0.0f;

    static float TWO_PI = Mathf.PI * 2;

    // Constructor.
    public Oscillator(float magnitude, float speed, WaveformFunction waveform)
    {
        this.magnitude = magnitude;
        this.speed = speed;
        this.waveform = waveform;
    }

    // Makes the oscillator progress through time by a given amount
    // and then returns its amplitude at the new time.
    public float SampleAmplitude(float deltaTime)
    {
        progress += deltaTime * speed;
        while (progress > TWO_PI)
        {
            progress -= TWO_PI;
        }
        while (progress < 0.0f)
        {
            progress += TWO_PI;
        }
        return magnitude * waveform(progress);
    }

    // Returns the difference in amplitude between the current time's amplitude
    // (current time as in the time of calling this method)
    // and the amplitude generated by stepping further through time.
    // Like SampleAmplitude, calling this method progresses the oscillator
    // further through time.
    public float SampleDelta(float deltaTime)
    {
        float currentValue = SampleAmplitude(0.0f);
        float nextValue = SampleAmplitude(deltaTime);
        return nextValue - currentValue;
    }
}