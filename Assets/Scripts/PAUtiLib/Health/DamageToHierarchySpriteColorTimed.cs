// Author(s): Paul Calande
// Script that causes a HierarchySpriteColorTimed to activate when a
// given object takes damage.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToHierarchySpriteColorTimed : MonoBehaviour
{
    [Tooltip("Health component that activates the color change when damaged.")]
    [SerializeField]
    Health health;
    [SerializeField]
    [Tooltip("Damage color component that is activated when damage is taken.")]
    HierarchySpriteColorTimed damageColor;

    private void Start()
    {
        health.Damaged += Health_Damaged;
    }

    private void Health_Damaged(int damage)
    {
        damageColor.ColorStart();
    }
}