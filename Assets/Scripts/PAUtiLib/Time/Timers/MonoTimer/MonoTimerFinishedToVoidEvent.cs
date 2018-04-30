// Author(s): Paul Calande
// Invokes a VoidEvent when a MonoTimer finishes.

using UnityEngine;

public class MonoTimerFinishedToVoidEvent : VoidEvent
{
    [SerializeField]
    [Tooltip("The timer will fire the VoidEvent each time it finishes.")]
    MonoTimer timer;

    private void Awake()
    {
        timer.SubscribeToFinished(Fire);
    }
    
    private void Fire(float secondsOverflow)
    {
        OnFired();
    }
}