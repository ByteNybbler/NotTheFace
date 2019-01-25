// Author(s): Paul Calande
// Invokes a trigger event when the trigger is entered by certain objects.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventEnter2D : TriggerEvent2D
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TryInvokeEvent(collision);
    }
}