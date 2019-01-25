// Author(s): Paul Calande
// Converts periodic integers to periodic damage values.
// Useful for creating real-time status effects like poison damage.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoTimedIntTimersToDamage : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The health to apply damage to.")]
    Health health;
    [SerializeField]
    [Tooltip("The periodic integer timers to retrieve damage values from.")]
    MonoTimedIntTimers timers;
    [SerializeField]
    [Tooltip("The value change text creator to use, if applicable.")]
    ValueChangeTextCreator textCreator;

    private void Start()
    {
        timers.TimerFinished += TimerFinished;
    }

    private void TimerFinished(float secondsOverflow, int damage)
    {
        health.Damage(damage);
        if (textCreator != null)
        {
            textCreator.Create(damage, transform.position);
        }
    }
}