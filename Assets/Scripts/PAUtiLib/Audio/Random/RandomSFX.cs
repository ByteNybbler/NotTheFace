// Author(s): Paul Calande
// Plays a random sound effect from an array.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSFX : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The array of sounds to randomly choose from.")]
    SOAAudioClip sounds;

    public void Fire()
    {
        AudioController ac = ServiceLocator.GetAudioController();
        if (ac != null)
        {
            ac.PlaySFX(sounds.GetRandomElement());
        }
    }
}