// Author(s): Paul Calande
// Changes the active state of an object.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activation : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The GameObject to change the active state of.")]
    GameObject toChange;
    [SerializeField]
    [Tooltip("The new active state. True is enabled, false is disabled.")]
    bool newState;

    public void Fire()
    {
        toChange.SetActive(newState);
    }
}