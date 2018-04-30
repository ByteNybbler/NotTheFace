// Author(s): Paul Calande
// Destroys the other object from the root when it invokes the trigger event.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventDestroyOther2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The trigger event to subscribe to.")]
    TriggerEvent2D triggerEvent;

    private void Awake()
    {
        triggerEvent.Subscribe(Fire);
    }

    private void Fire(Collider2D collision)
    {
        Destroy(collision.transform.root.gameObject);
    }
}