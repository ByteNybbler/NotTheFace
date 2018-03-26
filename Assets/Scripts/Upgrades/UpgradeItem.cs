// Author(s): Paul Calande
// Upgrade item.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeItem : MonoBehaviour
{
    public delegate void CollectedHandler();
    public event CollectedHandler Collected;

    private void OnCollected()
    {
        if (Collected != null)
        {
            Collected();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tongue"))
        {
            OnCollected();
        }
    }
}