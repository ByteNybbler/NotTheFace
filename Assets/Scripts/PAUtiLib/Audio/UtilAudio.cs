// Author(s): Paul Calande
// Utility functions for audio.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class UtilAudio
{
    // The minimum amount of decibels that a mixer can reach.
    // This value should correspond to the audio being muted.
    private const float minimumDecibels = -80.0f;

    // Convert a volume percentage (0.0 to 1.0) to decibels.
    public static float PercentToDecibels(float percent)
    {
        return Mathf.Max(minimumDecibels, 40.0f * Mathf.Log10(percent));
    }

    // Convert decibels to a volume percentage (0.0 to 1.0).
    public static float DecibelsToPercent(float db)
    {
        if (db == minimumDecibels)
        {
            return 0.0f;
        }
        else
        {
            return Mathf.Pow(10, db / 40.0f);
        }
    }

    // Get the name of a parameter appended to the name of its corresponding mixer.
    // Useful for creating a unique string for any given parameter.
    public static string GetFullParameterName(string mixerName, string parameterName)
    {
        return mixerName + "." + parameterName;
    }

    // Set the given mixer volume parameter to the value that was saved.
    // If the value was not saved, don't change the mixer parameter.
    public static void LoadVolumeIntoMixer(AudioMixer mixer, string parameterName)
    {
        string key = GetFullParameterName(mixer.name, parameterName);
        if (PlayerPrefs.HasKey(key))
        {
            float value = PlayerPrefs.GetFloat(key);
            SetMixerVolume(mixer, parameterName, value);
        }
    }

    // Change the volume of an audio mixer group based on a percentage.
    public static void SetMixerVolume(AudioMixer mixer, string parameterName, float percent)
    {
        float db = PercentToDecibels(percent);
        mixer.SetFloat(parameterName, db);
    }
}