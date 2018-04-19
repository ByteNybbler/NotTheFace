// Author(s): Paul Calande
// Taking damage causes a GameObject to change its active state for some time.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToActivationForTime : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The health component to watch for damage on.")]
    Health health;
    [SerializeField]
    [Tooltip("The component that will enable and disable the GameObject.")]
    ActivationForTime activationForTime;

    private void Start()
    {
        health.Damaged += x => activationForTime.Run();
    }
}