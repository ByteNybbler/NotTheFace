// Author(s): Paul Calande
// Script that makes it possible to take damage from the player.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFromPlayer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The health component for this enemy to use.")]
    Health health;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tongue"))
        {
            health.Damage(20);
        }
        if (collision.CompareTag("Player"))
        {
            PlayerInput pi = collision.GetComponent<PlayerInput>();
            if (pi.IsHeadbutting())
            {
                health.Damage(40);
            }
        }
    }
}