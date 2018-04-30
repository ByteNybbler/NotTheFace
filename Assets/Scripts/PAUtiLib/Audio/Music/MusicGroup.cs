// Author(s): Paul Calande
// A group of individual channels that make up one music track.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicGroup
{
    List<AudioSourceFader> channels = new List<AudioSourceFader>();

    public void AddChannel(AudioSource source, AudioClip clip)
    {
        AudioSourceFader fader = new AudioSourceFader(source);
        fader.Play(clip);
        channels.Add(fader);
    }

    // Fade the volume of a single channel.
    public void FadeChannel(int channelNumber, float targetVolume,
        float secondsToFade = 0.0f, float secondsToFadeDelay = 0.0f)
    {
        channels[channelNumber].FadeWithDelay(secondsToFadeDelay, secondsToFade, targetVolume);
    }

    // Fade the volume of every channel in the music group.
    public void Fade(float targetVolume, float secondsToFade = 0.0f,
        float secondsToFadeDelay = 0.0f)
    {
        for (int i = 0; i < channels.Count; ++i)
        {
            FadeChannel(i, targetVolume, secondsToFade, secondsToFadeDelay);
        }
    }

    // Forcefully stop all channels and clear them from the collection.
    public void Annihilate()
    {
        for (int i = 0; i < channels.Count; ++i)
        {
            channels[i].Stop();
        }
        channels.Clear();
    }

    // Progresses the fading on every channel.
    public void Tick(float deltaTime)
    {
        for (int i = 0; i < channels.Count; ++i)
        {
            channels[i].Tick(deltaTime);
        }
    }

    // Returns the channel ID based on the given AudioClip.
    public int GetChannelIndexFromClip(AudioClip clip)
    {
        for (int i = 0; i < channels.Count; ++i)
        {
            if (channels[i].GetClip() == clip)
            {
                return i;
            }
        }
        // Channel was not found.
        return -1;
    }
}