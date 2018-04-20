// Author(s): Paul Calande
// Invokes a VoidEvent when a fader finishes fading.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorAccessorFadeToVoidEvent : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The VoidEvent to fire.")]
    VoidEvent voidEvent;
    [SerializeField]
    [Tooltip("The fader that will fire the VoidEvent upon finishing fading.")]
    ColorAccessorFade fader;

    private void Start()
    {
        fader.Subscribe(Fire);
    }

    private void Fire(float secondsOverflow)
    {
        voidEvent.Fire();
    }
}