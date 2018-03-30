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
            int damage = collision.GetComponent<Damage>().Get();
            health.Damage(damage);
        }
        if (collision.CompareTag("Player"))
        {
            PlayerHeadbutt ph = collision.GetComponent<PlayerHeadbutt>();
            if (ph.IsHeadbutting())
            {
                health.Damage(ph.GetDamage());
            }
        }
    }
}