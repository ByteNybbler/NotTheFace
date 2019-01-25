// Author(s): Paul Calande
// Logs debug messages into the console when triggered.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventDebug2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The trigger event to subscribe to.")]
    TriggerEvent2D triggerEvent;

    private void Start()
    {
        triggerEvent.Subscribe(Triggered);
    }

    private void Triggered(Collider2D collision)
    {
        Debug.Log(name + " TriggerEvent with " + collision.name);
    }
}