// Author(s): Paul Calande
// Script which loads mixer volume values that were saved on the disk.
// These loaded values are used to set the given mixer parameters.
// The values are loaded in the Start method.
// Unfortunately, mixer parameters cannot be set within Awake.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerVolumeLoader : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The audio mixer to load the saved values into.")]
    AudioMixer audioMixer;
    [SerializeField]
    [Tooltip("An array of parameter names to be loaded into the mixer.")]
    string[] parameterNames;

    private void Start()
    {
        foreach (string parameter in parameterNames)
        {
            UtilAudio.LoadVolumeIntoMixer(audioMixer, parameter);
        }
    }
}