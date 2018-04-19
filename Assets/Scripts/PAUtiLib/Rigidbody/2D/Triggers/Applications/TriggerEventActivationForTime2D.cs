// Author(s): Paul Calande
// Makes a GameObject change its active state for some time when a trigger event occurs.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventActivationForTime2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The trigger event that will lead to the given GameObject's disabling.")]
    TriggerEvent2D triggerEvent;
    [SerializeField]
    [Tooltip("The component that will change the active state of the GameObject.")]
    ActivationForTime activationForTime;

    private void Start()
    {
        triggerEvent.Subscribe(x => activationForTime.Run());
    }
}