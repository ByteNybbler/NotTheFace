// Author(s): Paul Calande
// Assigns a value to an accessor when a VoidEvent is invoked.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidEventToSingleAccessorAssign<TValue, TAccessor> : MonoBehaviour
    where TAccessor : SingleAccessor<TValue>
{
    [SerializeField]
    [Tooltip("The VoidEvent to subscribe to.")]
    VoidEvent voidEvent;
    [SerializeField]
    [Tooltip("The accessor to change the value of.")]
    TAccessor accessor;
    [SerializeField]
    [Tooltip("The value to assign.")]
    TValue value;

    private void Awake()
    {
        voidEvent.Subscribe(Fire);
    }

    private void Fire()
    {
        accessor.Set(value);
    }
}