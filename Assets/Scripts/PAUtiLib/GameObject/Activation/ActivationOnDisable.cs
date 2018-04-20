// Author(s): Paul Calande
// Changes the active state of the given GameObject when this one is disabled.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationOnDisable : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The GameObject to change the active state of when this one is disabled.")]
    GameObject toChange;
    [SerializeField]
    [Tooltip("The new active state of the changed GameObject. False is disabled, true is enabled.")]
    bool newActiveState;

    private void OnDisable()
    {
        toChange.SetActive(newActiveState);
    }
}