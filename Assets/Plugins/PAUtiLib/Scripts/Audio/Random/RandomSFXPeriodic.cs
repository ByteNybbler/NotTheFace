// Author(s): Paul Calande
// Plays a random sound effect periodically.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSFXPeriodic : MonoBehaviour
{
    [SerializeField]
    TimeScale timeScale;
    [SerializeField]
    [Tooltip("The array of sounds to randomly choose from.")]
    SOAAudioClip sounds;
    [SerializeField]
    [Tooltip("How many seconds of silence occurs between each sound.")]
    float secondsBetweenSounds;
    [SerializeField]
    [Tooltip("The variance in the silence.")]
    float secondsBetweenSoundsVariance;

    // The timer that plays the sounds periodically.
    Timer timer;
    // The AudioController to play sounds on.
    AudioController ac;

    private void Start()
    {
        ac = ServiceLocator.GetAudioController();
        timer = new Timer(UtilRandom.RangeWithCenter(
            secondsBetweenSounds, secondsBetweenSoundsVariance),
            TimerFinished, true);
        timer.Run();
    }

    private void Fire()
    {
        AudioClip clip = sounds.GetRandomElement();
        ac.PlaySFX(clip);
        timer.SetSecondsTarget(clip.length + UtilRandom.RangeWithCenter(
            secondsBetweenSounds, secondsBetweenSoundsVariance));
    }

    private void TimerFinished(float secondsOverflow)
    {
        Fire();
    }

    private void FixedUpdate()
    {
        timer.Tick(timeScale.DeltaTime());
    }
}