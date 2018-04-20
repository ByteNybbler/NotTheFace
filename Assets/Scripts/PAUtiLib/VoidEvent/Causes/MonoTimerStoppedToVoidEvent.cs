// Author(s): Paul Calande
// Invokes a VoidEvent when a MonoTimer stops running.

using UnityEngine;

public class MonoTimerStoppedToVoidEvent : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The VoidEvent to fire.")]
    VoidEvent voidEvent;
    [SerializeField]
    [Tooltip("The timer will fire the VoidEvent each time it stops running.")]
    MonoTimer timer;

    private void Awake()
    {
        timer.SubscribeToStopped(Fire);
    }

    private void Fire()
    {
        voidEvent.Fire();
    }
}