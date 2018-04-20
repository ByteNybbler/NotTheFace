// Author(s): Paul Calande
// The invoked VoidEvent causes a MonoTimer to run.

using UnityEngine;

public class VoidEventToMonoTimerRun : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The VoidEvent to subscribe to.")]
    VoidEvent voidEvent;
    [SerializeField]
    [Tooltip("The MonoTimer to run.")]
    MonoTimer timer;

    private void Awake()
    {
        voidEvent.Subscribe(Fire);
    }

    private void Fire()
    {
        timer.Run();
    }
}
