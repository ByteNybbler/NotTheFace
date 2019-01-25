// Author(s): Paul Calande
// Base class for playing a music group in the AudioController.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicGroupPlayer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The music to use.")]
    SOAMusicChannelData music;
    [SerializeField]
    [Tooltip("How many seconds the previous music group takes to fade out.")]
    float secondsFadeOut;
    [SerializeField]
    [Tooltip("How many seconds to wait before the fade in begins.")]
    float secondsFadeInDelay;
    [SerializeField]
    [Tooltip("How many seconds the new music group takes to fade in.")]
    float secondsFadeIn;

    // Plays the music group.
    public void Play()
    {
        AudioController ac = ServiceLocator.GetAudioController();
        ac.PlayMusicGroup(music, secondsFadeOut, secondsFadeInDelay, secondsFadeIn);
    }
}