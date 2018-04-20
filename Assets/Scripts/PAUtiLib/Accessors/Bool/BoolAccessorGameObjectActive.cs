// Author(s): Paul Calande
// BoolAccessor support for activating and deactivating GameObjects.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolAccessorGameObjectActive : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The accessor to connect to.")]
    BoolAccessor accessor;
    [SerializeField]
    [Tooltip("The GameObject to modify the activation of.")]
    GameObject target;

    private void Awake()
    {
        accessor.Subscribe(target.SetActive);
    }
}