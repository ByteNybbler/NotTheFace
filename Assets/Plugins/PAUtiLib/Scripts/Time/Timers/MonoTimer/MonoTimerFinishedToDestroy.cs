// Author(s): Paul Calande
// Destroys a GameObject when a MonoTimer finishes.

using UnityEngine;

public class MonoTimerFinishedToDestroy : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The GameObject to destroy.")]
    GameObject toDestroy;
    [SerializeField]
    [Tooltip("The timer will destroy the GameObject when it finishes.")]
    MonoTimer timer;

    private void Awake()
    {
        timer.SubscribeToFinished(Fire);
    }
    
    private void Fire(float secondsOverflow)
    {
        Destroy(toDestroy);
    }
}