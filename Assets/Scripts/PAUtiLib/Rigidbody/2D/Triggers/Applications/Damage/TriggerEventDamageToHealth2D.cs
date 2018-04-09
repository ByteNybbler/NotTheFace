// Author(s): Paul Calande
// Script that converts trigger damage to actual health damage.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventDamageToHealth2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The health component to apply changes to.")]
    Health health;
    [SerializeField]
    [Tooltip("The trigger event to subscribe to.")]
    TriggerEventDamage2D damageEvent;

    private void Start()
    {
        damageEvent.Damaged += health.Damage;
    }
}