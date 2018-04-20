// Author(s): Paul Calande
// Invokes a VoidEvent when a Health component gets damaged.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToVoidEvent : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The VoidEvent to fire.")]
    VoidEvent voidEvent;
    [SerializeField]
    [Tooltip("The health component to watch for damage on.")]
    Health health;

    private void Awake()
    {
        health.Damaged += Fire;
    }

    private void Fire(int damage)
    {
        voidEvent.Fire();
    }
}