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
    [Tooltip("The pool size to use for music groups. Allows for crossfading.")]
    int groupCountMusic = 3;
    [SerializeField]
    [Tooltip("How many channels are currently reserved for music.")]
    int channelCountMusic = 8;
    [SerializeField]
    [Tooltip("The current AudioSource pool size for one-shot sound effects.")]
    int channelCountSFX = 8;

    // A pool of all existing SFX channels.
    // The pool will grow larger as needed.
    AudioSourcePool channelsSFX;
    // A pool of all existing music channels.
    AudioSourcePool channelsMusic;
    // Only one music group can be playing at a time.
    MusicGroup[] groupsMusic;
    // The index of the current music group.
    int groupMusicCurrent = 0;
    // The latest music group data used to play music.
    SOAMusicChannelData latestMusicGroupData = null;

    private void Awake()
    {
        channelsSFX = new AudioSourcePool(gameObject, groupSFX, channelCountSFX);
        channelsMusic = new AudioSourcePool(gameObject, groupMusic, channelCountMusic,
            true, 0.0f);
        groupsMusic = new MusicGroup[groupCountMusic];
        for (int i = 0; i < groupCountMusic; ++i)
        {
            groupsMusic[i] = new MusicGroup();
        }
    }

    // Play one-shot audio using an SFX channel.
    public void PlaySFX(AudioClip clip)
    {
        AudioSource source = channelsSFX.GetFreeChannel();
        source.PlayOneShot(clip);
    }

    private MusicGroup GetCurrentMusicGroup()
    {
        return groupsMusic[groupMusicCurrent];
    }

    // Play music using the music channels.
    // If the currently-playing music group is the same as the given music group,
    // this method will have no effect.
    public void PlayMusicGroup(SOAMusicChannelData musicGroup,
        float secondsToFadeOut = 0.0f,
        float secondsToWaitBeforeFadeIn = 0.0f,
        float secondsToFadeIn = 0.0f)
    {
        MusicGroup currentMusicGroup = GetCurrentMusicGroup();
        int channelCount = musicGroup.GetLength();

        // Don't move to a new music group if the current one is playing
        // the same music group as the one passed to this method.
        if (musicGroup == latestMusicGroupData)
        {
            // Instead, fade the channels to the corresponding volumes.
            for (int i = 0; i < channelCount; ++i)
            {
                AudioClip clip = musicGroup.GetClip(i);
                float targetVolume = musicGroup.GetVolume(i);
                int channelNumber = currentMusicGroup.GetChannelIndexFromClip(clip);
                SetMusicChannelVolume(channelNumber, targetVolume, secondsToFadeIn);
            }
            // That's all we need to do.
            return;
        }

        latestMusicGroupData = musicGroup;

        // Fade out the old music group.
        currentMusicGroup.Fade(0.0f, secondsToFadeOut);

        // Increment the current music group index.
        ++groupMusicCurrent;
        groupMusicCurrent %= groupsMusic.Length;

        // Get the new current music group and clear its data.
        currentMusicGroup = GetCurrentMusicGroup();
        currentMusicGroup.Annihilate();

        // Populate the music group's channels.
        for (int i = 0; i < channelCount; ++i)
        {
            AudioClip clip = musicGroup.GetClip(i);
            float targetVolume = musicGroup.GetVolume(i);
            AudioSource source = channelsMusic.GetFreeChannel();
            source.volume = 0.0f;
            currentMusicGroup.AddChannel(source, clip);
            currentMusicGroup.FadeChannel(i, targetVolume, secondsToFadeIn,
                secondsToWaitBeforeFadeIn);
        }
    }

    public void StopMusicGroup(float secondsToFadeOut = 0.0f)
    {
        GetCurrentMusicGroup().Fade(0.0f, secondsToFadeOut);
    }

    public void SetMusicChannelVolume(int channelNumber, float targetVolume,
        float secondsToFade = 0.0f)
    {
        GetCurrentMusicGroup().FadeChannel(channelNumber, targetVolume,
            secondsToFade);
    }

    private void FixedUpdate()
    {
        float dt = Time.deltaTime;
        for (int i = 0; i < groupsMusic.Length; ++i)
        {
            groupsMusic[i].Tick(dt);
        }
    }
}