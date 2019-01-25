// Author(s): Paul Calande
// The trigger event modifies an UpDirection component.
// Useful for creating triggers that modify direction of gravity, etc.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventNewDownDirection2D : MonoBehaviour
{
    [SerializeField]
    TriggerEvent2D triggerEvent;
    [SerializeField]
    [Tooltip("The up direction to modify.")]
    UpDirection2D upDirection;

    private void Start()
    {
        triggerEvent.Subscribe(NewDownDirection);
    }

    private void NewDownDirection(Collider2D collision)
    {
        TriggerNewDownDirection2D dd = collision.GetComponent<TriggerNewDownDirection2D>();
        if (dd != null)
        {
            upDirection.SetDownAngle(dd.GetDownDirection());
        }
    }
}