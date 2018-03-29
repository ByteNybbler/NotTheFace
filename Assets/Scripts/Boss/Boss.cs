// Author(s): Paul Calande
// General script for a Not the Face boss.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public delegate void DiedHandler();
    public event DiedHandler Died;

    [SerializeField]
    [Tooltip("The health of the boss.")]
    Health healthBoss;
    [SerializeField]
    [Tooltip("Damage color component.")]
    HierarchySpriteColorTimed damageColor;

    private void Start()
    {
        healthBoss.Died += OnDied;
        healthBoss.Damaged += Health_Damaged;
    }

    // Callback function for the boss taking damage.
    private void Health_Damaged(int damage)
    {
        damageColor.ColorStart();
    }

    private void OnDied()
    {
        Destroy(gameObject);
        if (Died != null)
        {
            Died();
        }
    }
}