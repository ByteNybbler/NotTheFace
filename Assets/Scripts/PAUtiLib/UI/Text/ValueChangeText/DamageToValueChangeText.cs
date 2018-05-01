// Author(s): Paul Calande
// Converts damage taken to value change text.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToValueChangeText : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The value change text creator to use.")]
    ValueChangeTextCreator creator;
    [SerializeField]
    [Tooltip("The health component to watch for damage on.")]
    Health health;

    private void Awake()
    {
        health.Damaged += TakeDamage;
    }

    private void TakeDamage(int damage)
    {
        creator.Create(damage, transform.position);
    }
}
