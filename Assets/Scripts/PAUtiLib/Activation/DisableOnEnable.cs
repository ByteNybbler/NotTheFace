// Author(s): Paul Calande
// Disables a given GameObject when this one is enabled.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnEnable : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The GameObject to disable when this one is enabled.")]
    GameObject toDisable;

    private void OnEnable()
    {
        toDisable.SetActive(false);
    }
}