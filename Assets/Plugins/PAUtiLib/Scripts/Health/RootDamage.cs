// Author(s): Paul Calande
// Accessor for damage, to be placed at the root of a GameObject.
// Useful for adjusting the damage of instantiated objects when the actual Damage
// component is not at the root of the hierarchy.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootDamage : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Reference to the object's Damage component.")]
    Damage damage;

    public int Get()
    {
        return damage.Get();
    }

    public void Add(int amount)
    {
        damage.Add(amount);
    }
}