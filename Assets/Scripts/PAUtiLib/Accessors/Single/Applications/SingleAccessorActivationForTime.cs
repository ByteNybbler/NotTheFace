// Author(s): Paul Calande
// Changes an accessor value when DisableForTime either enables or disables its GameObject.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAccessorActivationForTime<TValue, TAccessor> : MonoBehaviour
    where TAccessor : SingleAccessor<TValue>
{
    [SerializeField]
    [Tooltip("The component to use to modify the value.")]
    TAccessor accessor;
    [SerializeField]
    [Tooltip("The enabler/disabler to subscribe to.")]
    ActivationForTime activationForTime;
    [SerializeField]
    [Tooltip("The value to use when the GameObject is enabled.")]
    TValue valueNormal;
    [SerializeField]
    [Tooltip("The value to use when the GameObject is disabled.")]
    TValue valueDisabled;

    private void Start()
    {
        activationForTime.Subscribe(DisableForTime_StateChanged);
    }

    private void DisableForTime_StateChanged(bool active)
    {
        if (active)
        {
            Set(valueNormal);
        }
        else
        {
            Set(valueDisabled);
        }
    }

    private void Set(TValue value)
    {
        accessor.Set(value);
    }
}