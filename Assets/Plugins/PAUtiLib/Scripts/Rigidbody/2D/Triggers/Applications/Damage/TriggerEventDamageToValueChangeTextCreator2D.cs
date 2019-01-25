// Author(s): Paul Calande
// Damage-based trigger event that creates value change text.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventDamageToValueChangeTextCreator2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The trigger event to subscribe to.")]
    TriggerEventDamage2D damageEvent;
    [SerializeField]
    [Tooltip("The component that creates the value change text.")]
    ValueChangeTextCreator creator;

    private void Start()
    {
        damageEvent.Damaged += Damaged;
    }

    private void Damaged(Collider2D collision, int amount)
    {
        creator.Create(amount, collision.transform.position);
    }
}