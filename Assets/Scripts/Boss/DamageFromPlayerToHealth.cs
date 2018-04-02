// Author(s): Paul Calande
// Script that converts DamageFromPlayer damage to actual health damage.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFromPlayerToHealth : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The health component for this enemy to use.")]
    Health health;
    [SerializeField]
    [Tooltip("The DamageFromPlayer component to subscribe to.")]
    DamageFromPlayer dfp;

    private void Start()
    {
        dfp.Damaged += health.Damage;
    }
}