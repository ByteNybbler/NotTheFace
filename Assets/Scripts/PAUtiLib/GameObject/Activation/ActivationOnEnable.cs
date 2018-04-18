// Author(s): Paul Calande
// Changes the active state of the given GameObject when this one is enabled.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationOnEnable : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The GameObject to change the active state of when this one is enabled.")]
    GameObject toChange;
    [SerializeField]
    [Tooltip("The new active state of the changed GameObject. False is disabled, true is enabled.")]
    bool newActiveState;

    private void OnEnable()
    {
        toChange.SetActive(newActiveState);
    }
}