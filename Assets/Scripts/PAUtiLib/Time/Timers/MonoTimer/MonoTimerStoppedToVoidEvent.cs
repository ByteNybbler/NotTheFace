// Author(s): Paul Calande
// Invokes a VoidEvent when a MonoTimer stops running.

using UnityEngine;

public class MonoTimerStoppedToVoidEvent : VoidEvent
{
    [SerializeField]
    [Tooltip("The timer will fire the VoidEvent each time it stops running.")]
    MonoTimer timer;

    private void Awake()
    {
        timer.SubscribeToStopped(Fire);
    }

    private void Fire()
    {
        OnFired();
    }
}