// Author(s): Paul Calande
// Script that makes the AudioController play music on Start.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicOnStart : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The music to use.")]
    AudioClip music;

    private void Start()
    {
        AudioController ac = ServiceLocator.GetAudioController();
        ac.PlayMusic(music);
    }
}