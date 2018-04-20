// Author(s): Paul Calande
// Changes the active state of a given GameObject.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The GameObject to change the active state of.")]
    GameObject toChange;
    [SerializeField]
    [Tooltip("The new active state of the changed GameObject. False is disabled, true is enabled.")]
    bool newActiveState;
    
    public void Fire()
    {
        toChange.SetActive(newActiveState);
    }
}