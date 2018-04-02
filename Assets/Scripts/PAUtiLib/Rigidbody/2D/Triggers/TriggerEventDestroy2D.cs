// Author(s): Paul Calande
// The trigger event will make a certain gameObject be destroyed.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventDestroy2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The trigger event that will lead to the given GameObject's destruction.")]
    TriggerEvent2D triggerEvent;
    [SerializeField]
    [Tooltip("The GameObject to destroy.")]
    GameObject destroyThis;

    private void Start()
    {
        triggerEvent.Subscribe(DoDestroy);
    }

    private void DoDestroy(Collider2D collision)
    {
        Destroy(destroyThis);
    }
}