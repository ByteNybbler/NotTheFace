// Author(s): Paul Calande
// Assigns a given accessor a given value when the GameObject is disabled.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAccessorAssignOnDisable<TValue, TAccessor> : MonoBehaviour
    where TAccessor : SingleAccessor<TValue>
{
    [SerializeField]
    [Tooltip("The accessor to change the value of.")]
    TAccessor accessor;
    [SerializeField]
    [Tooltip("The value to assign.")]
    TValue value;
    
    private void OnDisable()
    {
        accessor.Set(value);
    }
}