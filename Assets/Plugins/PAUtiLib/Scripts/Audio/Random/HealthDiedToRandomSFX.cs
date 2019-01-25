// Author(s): Paul Calande
// Plays a random sound effect when the given object dies.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDiedToRandomSFX : RandomSFX
{
    [SerializeField]
    [Tooltip("The health component to watch for death on.")]
    Health health;

    private void Awake()
    {
        health.Died += Fire;
    }
}