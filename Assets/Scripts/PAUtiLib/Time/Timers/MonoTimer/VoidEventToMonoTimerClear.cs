// Author(s): Paul Calande
// The invoked VoidEvent causes a MonoTimer to clear.

using UnityEngine;

public class VoidEventToMonoTimerClear : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The VoidEvent to subscribe to.")]
    VoidEvent voidEvent;
    [SerializeField]
    [Tooltip("The MonoTimer to clear.")]
    MonoTimer timer;

    private void Awake()
    {
        voidEvent.Subscribe(Fire);
    }

    private void Fire()
    {
        timer.Clear();
    }
}
