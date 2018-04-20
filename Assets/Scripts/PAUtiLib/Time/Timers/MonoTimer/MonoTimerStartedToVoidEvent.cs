// Author(s): Paul Calande
// Invokes a VoidEvent when a MonoTimer starts running.

using UnityEngine;

public class MonoTimerStartedToVoidEvent : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The VoidEvent to fire.")]
    VoidEvent voidEvent;
    [SerializeField]
    [Tooltip("The timer will fire the VoidEvent each time it starts running.")]
    MonoTimer timer;

    private void Awake()
    {
        timer.SubscribeToStarted(Fire);
    }

    private void Fire()
    {
        voidEvent.Fire();
    }
}