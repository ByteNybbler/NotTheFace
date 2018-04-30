// Author(s): Paul Calande
// Invokes a VoidEvent when this GameObject is started.

using UnityEngine;

public class StartToVoidEvent : VoidEvent
{
    private void Start()
    {
        OnFired();
    }
}