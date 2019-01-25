// Author(s): Paul Calande
// Validator for an Instantiator.
// Checks if the given InstantiatedDamage has a nonzero damage value.
// If it does have nonzero damage, then this condition is satisfied.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateIfDamageNonzero : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The Instantiator to validate.")]
    Instantiator instantiator;
    [SerializeField]
    [Tooltip("The component to check the damage value of.")]
    InstantiatedDamage instantiatedDamage;

    private void Start()
    {
        instantiator.ValidationStarted += Check;
    }

    private void Check()
    {
        if (instantiatedDamage.Get() == 0)
        {
            instantiator.Invalidate();
        }
    }
}