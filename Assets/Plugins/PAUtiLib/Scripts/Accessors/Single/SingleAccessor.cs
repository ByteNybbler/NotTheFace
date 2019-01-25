// Author(s): Paul Calande
// Accesses a single variable common to any number of supported components. 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SingleAccessor<TValue> : MonoBehaviour
{
    // Invoked when the variable is set.
    public delegate void ValueSetHandler(TValue value);
    event ValueSetHandler ValueSet;

    [SerializeField]
    [Tooltip("The current value maintained in the accessor.")]
    TValue value;

    public void Subscribe(ValueSetHandler SetCallback)
    {
        ValueSet += SetCallback;
        // Update the value for the subscriber to this component.
        SetCallback(value);
    }

    public void Unsubscribe(ValueSetHandler SetCallback)
    {
        ValueSet -= SetCallback;
    }

    public void Set(TValue value)
    {
        this.value = value;
        OnValueSet();
        //Debug.Log(name + " SingleAccessor Set value: " + value);
    }

    public TValue Get()
    {
        return value;
    }

    private void OnValueSet()
    {
        if (ValueSet != null)
        {
            ValueSet(value);
        }
    }

    // Called by Animator components.
    private void OnDidApplyAnimationProperties()
    {
        OnValueSet();
    }
}