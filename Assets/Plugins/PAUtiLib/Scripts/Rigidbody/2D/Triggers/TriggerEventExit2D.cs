// Author(s): Paul Calande
// Invokes a trigger event when the trigger is exited by certain objects.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventExit2D : TriggerEvent2D
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        TryInvokeEvent(collision);
    }
}