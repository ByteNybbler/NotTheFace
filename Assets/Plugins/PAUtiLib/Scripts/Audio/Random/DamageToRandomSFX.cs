// Author(s): Paul Calande
// Plays a random sound effect when the given object takes damage.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToRandomSFX : RandomSFX
{
    [SerializeField]
    [Tooltip("The health component to watch for damage on.")]
    Health health;

    private void Awake()
    {
        health.Damaged += Damaged;
    }

    private void Damaged(int damage)
    {
        Fire();
    }
}