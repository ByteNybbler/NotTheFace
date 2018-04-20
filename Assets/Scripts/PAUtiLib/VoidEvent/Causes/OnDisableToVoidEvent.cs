// Author(s): Paul Calande
// Invokes a VoidEvent when this GameObject is disabled.

using UnityEngine;

public class OnDisableToVoidEvent : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The VoidEvent to fire.")]
    VoidEvent voidEvent;

    private void OnDisable()
    {
        voidEvent.Fire();
    }
}