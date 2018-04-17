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
    [Tooltip("The GameObject to instantiate when taking damage.")]
    GameObject toInstantiate;

    private void Start()
    {
        health.Damaged += Health_Damaged;
    }

    private void Health_Damaged(int damage)
    {
        GameObject obj = Instantiate(toInstantiate, transform);
        obj.transform.position = transform.position;
    }
}