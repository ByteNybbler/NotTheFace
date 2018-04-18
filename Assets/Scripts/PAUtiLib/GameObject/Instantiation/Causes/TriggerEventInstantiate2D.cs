// Author(s): Paul Calande
// Runs an Instantiator based on a trigger event.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventInstantiate2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The trigger event to subscribe to.")]
    TriggerEvent2D triggerEvent;
    [SerializeField]
    [Tooltip("Instantiates a GameObject when the trigger event is invoked.")]
    Instantiator instantiator;

    private void Start()
    {
        triggerEvent.Subscribe(x => instantiator.Instantiate());
    }
}