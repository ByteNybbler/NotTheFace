// Author(s): Paul Calande
// Converts a trigger event to a VoidEvent.

using UnityEngine;

public class TriggerEvent2DToVoidEvent : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The VoidEvent to fire.")]
    VoidEvent voidEvent;
    [SerializeField]
    [Tooltip("The trigger event that will trigger the VoidEvent.")]
    TriggerEvent2D triggerEvent;

    private void Start()
    {
        triggerEvent.Subscribe(Fire);
    }

    private void Fire(Collider2D collision)
    {
        voidEvent.Fire();
    }
}