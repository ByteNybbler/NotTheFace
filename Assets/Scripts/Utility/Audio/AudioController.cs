// Author(s): Paul Calande
// Script that manages audio channels and plays audio.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The Audio Mixer Group to use for music.")]
    AudioMixerGroup groupMusic;
    [SerializeField]
    [Tooltip("The Audio Mixer Group to use for sound effects.")]
    AudioMixerGroup groupSFX;
    [SerializeField]
    [Tooltip("The current AudioSource pool size for one-shot sound effects.")]
    int channelCountSFX;

    // A pool of all existing SFX channels.
    // The pool will grow larger as needed.
    List<AudioSource> channelsSFX = new List<AudioSource>();
    // The channel to use for playing music.
    // Because this is only a single channel, only one music track can be playing at a time.
    AudioSource channelMusic;

    private void Awake()
    {
        // Populate the SFX channel pool based on the initially-defined SFX channel count.
        for (int i = 0; i < channelCountSFX; ++i)
        {
            AddNewSFXChannel();
        }
        // Instantiate the music channel.
        channelMusic = gameObject.AddComponent<AudioSource>();
        channelMusic.outputAudioMixerGroup = groupMusic;
        channelMusic.loop = true;
    }

    // Add a new SFX channel to the SFX channel pool and return this new channel.
    private AudioSource AddNewSFXChannel()
    {
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        newSource.outputAudioMixerGroup = groupSFX;
        channelsSFX.Add(newSource);
        return newSource;
    }

    // Returns a free channel from the SFX channel pool.
    // In this case, "free" means "not currently playing audio".
    private AudioSource GetFreeSFXChannel()
    {
        foreach (AudioSource source in channelsSFX)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }
        // No free SFX channels have been found, so add a new one to the SFX channel pool.
        return AddNewSFXChannel();
    }

    // Play one-shot audio using an SFX channel.
    public void PlaySFX(AudioClip clip)
    {
        AudioSource source = GetFreeSFXChannel();
        source.PlayOneShot(clip);
    }

    // Play music using the music channel.
    // If the currently-playing music is the same as the given argument,
    // this method will have no effect.
    public void PlayMusic(AudioClip clip)
    {
        AudioClip currentClip = channelMusic.clip;
        if (!channelMusic.isPlaying || clip != currentClip)
        {
            channelMusic.clip = clip;
            channelMusic.Play();
        }
    }

    public void StopMusic()
    {
        channelMusic.Stop();
    }
}