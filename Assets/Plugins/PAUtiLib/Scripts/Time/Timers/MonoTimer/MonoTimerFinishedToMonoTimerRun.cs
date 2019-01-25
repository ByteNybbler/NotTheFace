// Author(s): Paul Calande
// Runs a MonoTimer when a MonoTimer finishes.
// This can effectively be used to create a chain of MonoTimers.

using UnityEngine;

public class MonoTimerFinishedToMonoTimerRun : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The MonoTimer that will start the new MonoTimer when it finishes.")]
    MonoTimer timerToFinish;
    [SerializeField]
    [Tooltip("The MonoTimer that will be run when the other MonoTimer finishes.")]
    MonoTimer timerToRun;

    private void Awake()
    {
        timerToFinish.SubscribeToFinished(Fire);
    }
    
    private void Fire(float secondsOverflow)
    {
        timerToRun.Run(secondsOverflow);
    }
}