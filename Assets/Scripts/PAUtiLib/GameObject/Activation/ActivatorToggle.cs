// Author(s): Paul Calande
// Toggles the active state of a given GameObject.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorToggle : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The GameObject to change the active state of.")]
    GameObject toChange;

    public void Fire()
    {
        toChange.SetActive(!toChange.activeSelf);
    }
}