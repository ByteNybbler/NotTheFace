// Author(s): Paul Calande
// Makes a GameObject be disabled for some time when a trigger event occurs.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventDisableForTime2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The trigger event that will lead to the given GameObject's disabling.")]
    TriggerEvent2D triggerEvent;
    [SerializeField]
    [Tooltip("The component that will disable the GameObject and later enable it again.")]
    DisableForTime disableForTime;

    private void Start()
    {
        triggerEvent.Subscribe(x => disableForTime.Run());
    }
}