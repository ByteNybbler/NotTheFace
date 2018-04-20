// Author(s): Paul Calande
// Causes an accessor value to change temporarily when a VoidEvent is invoked.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidEventToSingleAccessorChangeForTime
    <TValue, TAccessor, TTimer, TAccessorChangeForTime>
    : MonoBehaviour
    where TAccessor : SingleAccessor<TValue>
    where TTimer : MonoTimerChangeValueForTime<TValue>
    where TAccessorChangeForTime : SingleAccessorChangeForTime<TValue, TAccessor, TTimer>
{
    [SerializeField]
    [Tooltip("The VoidEvent to subscribe to.")]
    VoidEvent voidEvent;
    [SerializeField]
    [Tooltip("The component that will temporarily change the accessor value.")]
    TAccessorChangeForTime changer;

    private void Start()
    {
        voidEvent.Subscribe(changer.Run);
    }
}