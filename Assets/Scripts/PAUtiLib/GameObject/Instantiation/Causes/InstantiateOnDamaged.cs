// Author(s): Paul Calande
// Instantiates a GameObject when taking damage at this transform position.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateOnDamaged : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The health component to subscribe to.")]
    Health health;
    [SerializeField]
    [Tooltip("The instantiator to use when taking damage.")]
    Instantiator instantiator;

    private void Start()
    {
        health.Damaged += Health_Damaged;
    }

    private void Health_Damaged(int damage)
    {
        instantiator.Instantiate();
    }
}