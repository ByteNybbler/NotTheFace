// Author(s): Paul Calande
// Base class for a script that sets a property on an instantiated object.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InstantiatedProperty<TValue> : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The instantiator to subscribe to.")]
    Instantiator instantiator;
    [SerializeField]
    [Tooltip("The center of the potential property values.")]
    protected TValue valueCenter;
    [SerializeField]
    [Tooltip("The radius of the potential property values, for randomization.")]
    protected TValue valueRadius;

    private void Awake()
    {
        instantiator.Instantiated += Instantiated;
    }

    protected abstract void Instantiated(GameObject obj, float secondsOverflow);
}
