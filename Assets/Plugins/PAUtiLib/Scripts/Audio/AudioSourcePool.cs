// Author(s): Paul Calande
// A pool of AudioSources.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSourcePool
{
    // The object that owns all the AudioSources.
    GameObject owner;
    // The mixer group to assign the AudioSources to.
    AudioMixerGroup mixerGroup;
    // The pool of AudioSources.
    List<AudioSource> channels = new List<AudioSource>();
    // Whether each AudioSource loops.
    bool loop;
    // The initial volume of each AudioSource.
    float volume;

    public AudioSourcePool(GameObject owner, AudioMixerGroup mixerGroup, int channelCount,
        bool loop = false, float volume = 1.0f)
    {
        this.owner = owner;
        this.mixerGroup = mixerGroup;
        this.loop = loop;
        this.volume = volume;
        // Populate the SFX channel pool based on the initially-defined SFX channel count.
        for (int i = 0; i < channelCount; ++i)
        {
            AddNewChannel();
        }
    }

    // Add a new channel to the channel pool and return this new channel.
    public AudioSource AddNewChannel()
    {
        AudioSource newSource = owner.AddComponent<AudioSource>();
        newSource.outputAudioMixerGroup = mixerGroup;
        newSource.loop = loop;
        newSource.volume = volume;
        channels.Add(newSource);
        return newSource;
    }

    // Returns a free channel from the channel pool.
    // In this case, "free" means "not currently playing audio".
    public AudioSource GetFreeChannel()
    {
        foreach (AudioSource source in channels)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }
        // No free channels have been found, so add a new one to the channel pool.
        return AddNewChannel();
    }
}