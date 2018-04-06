// Author(s): Paul Calande
// Destroys the GameObject when the timer runs out.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToDestroy : MonoBehaviour
{
    [SerializeField]
    TimeScale timeScale;

    Timer timerDestroy;

    // Set the number of seconds it will take for the object to be destroyed.
    // This also starts the timer.
    public void Set(float seconds)
    {
        timerDestroy = new Timer(seconds, TimerExpire);
    }

    private void TimerExpire(float secondsOverflow)
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        timerDestroy.Tick(timeScale.DeltaTime());
    }
}