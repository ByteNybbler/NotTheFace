// Author(s): Paul Calande
// Invokes a VoidEvent when this GameObject is disabled.

using UnityEngine;

public class OnDisableToVoidEvent : VoidEvent
{
    private void OnDisable()
    {
        OnFired();
    }
}