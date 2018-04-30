// Author(s): Paul Calande
// Invokes a VoidEvent when this GameObject is enabled.
//
// Needs to be executed later than VoidEvent subscriber scripts in the Script
// Execution Order. This is because other components will need time to
// subscribe before OnEnabled gets called in this script.

using UnityEngine;

public class OnEnableToVoidEvent : VoidEvent
{
    private void OnEnable()
    {
        OnFired();
    }
}