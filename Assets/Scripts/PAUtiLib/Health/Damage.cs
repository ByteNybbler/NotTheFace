// Author(s): Paul Calande
// Damage component which determines how much damage an object does.
// This component is meant to be retrieved in collisions.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField]
    [Tooltip("How much damage the object does.")]
    int damage = 0;

    public int Get()
    {
        return damage;
    }

    public void Add(int amount)
    {
        damage += amount;
    }
}