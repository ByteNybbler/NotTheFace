// Author(s): Paul Calande
// Invokes a VoidEvent when a MonoTimer finishes.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoTimerToVoidEvent : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The VoidEvent to fire.")]
    VoidEvent voidEvent;
    [SerializeField]
    [Tooltip("The timer will fire the VoidEvent each time it finishes.")]
    MonoTimer timer;

    private void Awake()
    {
        timer.Subscribe(Fire);
    }

    private void Fire(float secondsOverflow)
    {
        voidEvent.Fire();
    }
}