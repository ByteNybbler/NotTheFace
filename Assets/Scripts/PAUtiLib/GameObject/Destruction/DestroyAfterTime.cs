// Author(s): Paul Calande
// Destroys the GameObject after a certain number of seconds have passed.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField]
    TimeScale timeScale;
    [SerializeField]
    [Tooltip("How many seconds should pass before the GameObject gets destroyed.")]
    float secondsBeforeDestroying;

    Timer timerDestroy;

    private void Start()
    {
        timerDestroy = new Timer(secondsBeforeDestroying, DestroyObject);
    }

    private void DestroyObject(float secondsOverflow)
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        timerDestroy.Tick(timeScale.DeltaTime());
    }
}