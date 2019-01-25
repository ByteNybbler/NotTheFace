// Author(s): Paul Calande
// Fires an instantiator when an object runs out of health.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOnDiedToInstantiator : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The instantiator to fire.")]
    Instantiator instantiator;
    [SerializeField]
    [Tooltip("The health component to track.")]
    Health health;

    private void Awake()
    {
        health.Died += Fire;
    }

    private void Fire()
    {
        instantiator.Instantiate();
    }
}