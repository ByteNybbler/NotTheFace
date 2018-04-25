// Author(s): Paul Calande
// Script that converts trigger damage to actual health damage.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventDamageToHealth2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The trigger event to subscribe to.")]
    TriggerEventDamage2D damageEvent;
    [SerializeField]
    [Tooltip("The health component to apply changes to.")]
    Health health;

    private void Start()
    {
        damageEvent.Damaged += Damaged;
    }

    private void Damaged(Collider2D collision, int amount)
    {
        health.Damage(amount);
    }
}