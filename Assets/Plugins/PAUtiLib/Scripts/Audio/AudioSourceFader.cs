// Author(s): Paul Calande
// Class that can fade an AudioSource's volume in and out.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceFader
{
    // The wrapped AudioSource.
    AudioSource source;
    // The timer to use for fading in and out.
    Timer timerFade;
    // The interpolator to use for adjusting volume.
    InterpolateFromCurrent<float> interpolator;

    // The timer that potentially runs prior to the fading timer.
    Timer timerBeforeFade;
    // How many seconds the volume takes to fade after timerBeforeFade finishes.
    float secondsToFadeAfterDelay;
    // The volume to fade to after timerBeforeFade finishes.
    float targetVolumeAfterDelay;

    public AudioSourceFader(AudioSource source)
    {
        this.source = source;
        timerFade = new Timer(1.0f, null, false, true);
        timerBeforeFade = new Timer(1.0f, TimerBeforeFadeFinished, false, true);
        interpolator = new InterpolateFromCurrent<float>(Mathf.Lerp, timerFade,
            SetVolume, GetVolume);
    }

    private void SetVolume(float volume)
    {
        source.volume = volume;
    }

    private float GetVolume()
    {
        return source.volume;
    }

    public AudioClip GetClip()
    {
        return source.clip;
    }

    public void Fade(float secondsToFade, float targetVolume)
    {
        timerFade.Stop();
        timerBeforeFade.Stop();
        if (secondsToFade == 0)
        {
            SetVolume(targetVolume);
        }
        else
        {
            timerFade.SetSecondsTarget(secondsToFade);
            timerFade.Run();
            interpolator.SetTargetValue(targetVolume);
        }
    }

    public void FadeOut(float secondsToFadeOut)
    {
        Fade(secondsToFadeOut, 0.0f);
    }

    public void FadeIn(float secondsToFadeIn)
    {
        Fade(secondsToFadeIn, 1.0f);
    }

    public void FadeWithDelay(float secondsDelay, float secondsToFade, float targetVolume)
    {
        if (secondsDelay == 0.0f)
        {
            Fade(secondsToFade, targetVolume);
        }
        else
        {
            secondsToFadeAfterDelay = secondsToFade;
            targetVolumeAfterDelay = targetVolume;
            timerBeforeFade.Run();
        }
    }

    public void FadeInWithDelay(float secondsDelay, float secondsToFadeIn)
    {
        FadeWithDelay(secondsDelay, secondsToFadeIn, 1.0f);
    }

    public void FadeOutWithDelay(float secondsDelay, float secondsToFadeIn)
    {
        FadeWithDelay(secondsDelay, secondsToFadeIn, 0.0f);
    }

    private void TimerBeforeFadeFinished(float secondsOverflow)
    {
        Fade(secondsToFadeAfterDelay, targetVolumeAfterDelay);
    }

    // Play the given AudioClip on the AudioSource.
    public void Play(AudioClip clip)
    {
        if (source.clip != clip || !source.isPlaying)
        {
            source.Stop();
            source.clip = clip;
            source.Play();
        }
    }

    // Stop playing the AudioClip on the AudioSource.
    public void Stop()
    {
        source.Stop();
    }

    public void Tick(float deltaTime)
    {
        timerFade.Tick(deltaTime);
        timerBeforeFade.Tick(deltaTime);
    }
}