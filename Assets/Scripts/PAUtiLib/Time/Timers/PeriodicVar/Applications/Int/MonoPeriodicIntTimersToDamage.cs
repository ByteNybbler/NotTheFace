﻿// Author(s): Paul Calande
// Converts periodic integers to periodic damage values.
// Useful for creating real-time status effects like poison damage.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoPeriodicIntTimersToDamage : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The health to apply damage to.")]
    Health health;
    [SerializeField]
    [Tooltip("The periodic integer timers to retrieve damage values from.")]
    MonoPeriodicIntTimers timers;

    private void Start()
    {
        timers.TimerFinished += TimerFinished;
    }

    private void TimerFinished(float secondsOverflow, int damage)
    {
        health.Damage(damage);
    }
}