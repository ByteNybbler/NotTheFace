// Author(s): Paul Calande
// Script that makes it possible to take damage from the player in Not the Face.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFromPlayer : MonoBehaviour
{
    // Invoked when this component takes damage. The parameter is the amount of damage.
    public delegate void DamagedHandler(int amount);
    public event DamagedHandler Damaged;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tongue"))
        {
            int damage = collision.GetComponent<Damage>().Get();
            OnDamaged(damage);
        }
        if (collision.CompareTag("Player"))
        {
            PlayerHeadbutt ph = collision.GetComponent<PlayerHeadbutt>();
            if (ph.IsHeadbutting())
            {
                OnDamaged(ph.GetDamage());
            }
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