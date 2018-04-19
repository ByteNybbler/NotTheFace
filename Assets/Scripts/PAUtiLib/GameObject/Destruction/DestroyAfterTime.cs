// Author(s): Paul Calande
// Destroys the GameObject after a certain number of seconds have passed.
// The timer starts automatically.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField]
    TimeScale timeScale;
    [SerializeField]
    [Tooltip("The GameObject to destroy when time runs out.")]
    GameObject toDestroy;
    [SerializeField]
    [Tooltip("How many seconds should pass before the GameObject gets destroyed.")]
    float secondsBeforeDestroying;

    Timer timerDestroy;

    public void SetTargetTime(float secondsBeforeDestroying)
    {
        this.secondsBeforeDestroying = secondsBeforeDestroying;
        if (timerDestroy != null)
        {
            timerDestroy.SetTargetTime(secondsBeforeDestroying);
        }
    }

    public float GetTargetTime()
    {
        return secondsBeforeDestroying;
    }

    private void Start()
    {
        timerDestroy = new Timer(secondsBeforeDestroying, DestroyObject, false);
        timerDestroy.Start();
    }

    private void DestroyObject(float secondsOverflow)
    {
        Destroy(toDestroy);
    }

    private void FixedUpdate()
    {
        timerDestroy.Tick(timeScale.DeltaTime());
    }
}