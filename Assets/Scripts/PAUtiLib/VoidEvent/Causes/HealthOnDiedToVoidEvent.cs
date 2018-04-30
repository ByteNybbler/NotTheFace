// Author(s): Paul Calande
// Invokes a VoidEvent when an object runs out of health.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOnDiedToVoidEvent : VoidEvent
{
    [SerializeField]
    [Tooltip("The health component to track.")]
    Health health;

    private void Awake()
    {
        health.Died += OnFired;
    }
}