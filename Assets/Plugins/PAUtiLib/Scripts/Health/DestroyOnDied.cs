// Author(s): Paul Calande
// Destroys this GameObject when it runs out of health.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDied : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The health to track.")]
    Health health;

    private void Start()
    {
        health.Died += Health_Died;
    }

    private void Health_Died()
    {
        Destroy(gameObject);
    }
}