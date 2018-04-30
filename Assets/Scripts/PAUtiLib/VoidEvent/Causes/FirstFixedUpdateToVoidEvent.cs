// Author(s): Paul Calande
// Fires a VoidEvent during the first FixedUpdate after this GameObject is enabled.
// This occurs each time the GameObject is enabled.
// Useful for scenarios such as disabling a GameObject after letting it exist for one frame.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstFixedUpdateToVoidEvent : VoidEvent
{
    bool ready = true;

    private void FixedUpdate()
    {
        if (ready)
        {
            OnFired();
            ready = false;
        }
    }

    private void OnEnable()
    {
        ready = true;
    }
}