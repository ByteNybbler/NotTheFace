// Author(s): Paul Calande
// Invokes a VoidEvent when a Health component gets damaged.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToVoidEvent : VoidEvent
{
    [SerializeField]
    [Tooltip("The health component to watch for damage on.")]
    Health health;

    private void Awake()
    {
        health.Damaged += Fire;
    }

    private void Fire(int damage)
    {
        OnFired();
    }
}