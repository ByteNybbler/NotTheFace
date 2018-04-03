// Author(s): Paul Calande
// Script for taking damage when a Damage component enters the trigger.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventDamage2D : MonoBehaviour
{
    // Invoked when this component takes damage. The parameter is the amount of damage.
    public delegate void DamagedHandler(int amount);
    public event DamagedHandler Damaged;

    [SerializeField]
    TriggerEvent2D triggerEvent;

    private void Start()
    {
        triggerEvent.Subscribe(TriggerEvent_Invoked);
    }

    private void TriggerEvent_Invoked(Collider2D collision)
    {
        Damage damage = collision.GetComponent<Damage>();
        if (damage != null)
        {
            OnDamaged(damage.Get());
        }
    }

    private void OnDamaged(int amount)
    {
        if (Damaged != null)
        {
            Damaged(amount);
        }
    }
}