// Author(s): Paul Calande
// Sets the damage of an instantiated object.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatedDamage : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The instantiator to subscribe to.")]
    Instantiator instantiator;
    [SerializeField]
    [Tooltip("The damage value to set for the instantiated object via RootDamage.")]
    int damage = 0;

    private void Awake()
    {
        instantiator.Instantiated += Instantiated;
    }

    public int Get()
    {
        return damage;
    }

    public void Add(int amount)
    {
        damage += amount;
    }

    private void Instantiated(GameObject obj)
    {
        obj.GetComponent<RootDamage>().Add(damage);
    }
}