// Author(s): Paul Calande
// The invoked VoidEvent causes a MonoTimer to stop.

using UnityEngine;

public class VoidEventToMonoTimerStop : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The VoidEvent to subscribe to.")]
    VoidEvent voidEvent;
    [SerializeField]
    [Tooltip("The MonoTimer to stop.")]
    MonoTimer timer;

    private void Awake()
    {
        voidEvent.Subscribe(Fire);
    }

    private void Fire()
    {
        timer.Stop();
    }
}
