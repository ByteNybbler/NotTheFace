// Author(s): Paul Calande
// Base class used to construct a connection between an accessor and another component.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingleAccessorConnection<TValue, TAccessor, TConnected> : MonoBehaviour
    where TAccessor : SingleAccessor<TValue>
{
    [SerializeField]
    [Tooltip("The accessor to subscribe to.")]
    protected TAccessor accessor;
    [SerializeField]
    [Tooltip("The component subscribing to the accessor's set event.")]
    protected TConnected connected;
    [SerializeField]
    [Tooltip("Whether to use the connected component's data as the initial accessor value.")]
    bool useAsInitialValue;

    private void Awake()
    {
        if (useAsInitialValue)
        {
            accessor.Set(Get());
        }
        accessor.Subscribe(Set);
    }

    // Sets the value in the connected component.
    protected abstract void Set(TValue data);
    // Gets the value of the connected component.
    protected abstract TValue Get();
}