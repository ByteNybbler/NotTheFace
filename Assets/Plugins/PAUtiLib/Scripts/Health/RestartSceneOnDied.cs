// Author(s): Paul Calande
// Restarts the scene after a delay when a Health component dies.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartSceneOnDied : MonoBehaviour
{
    [SerializeField]
    TimeScale timeScale;
    [SerializeField]
    [Tooltip("The health component to track.")]
    Health health;
    [SerializeField]
    [Tooltip("Seconds to wait before restarting the scene once the health runs out.")]
    float secondsToWait = 3.0f;

    Timer timerRestart;

    private void Start()
    {
        timerRestart = new Timer(secondsToWait, TimerFinished, false);
        health.Died += Health_Died;
    }

    private void Health_Died()
    {
        timerRestart.Run();
    }

    private void TimerFinished(float secondsOverflow)
    {
        ServiceLocator.GetSceneTracker().RestartScene();
    }

    private void FixedUpdate()
    {
        timerRestart.Tick(timeScale.DeltaTime());
    }
}