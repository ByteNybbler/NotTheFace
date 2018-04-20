// Author(s): Paul Calande
// Accesses a single variable common to any number of supported components. 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SingleAccessor<T> : MonoBehaviour
{
    // Invoked when the variable is set.
    public delegate void ValueSetHandler(T value);
    event ValueSetHandler ValueSet;

    [SerializeField]
    [Tooltip("The current value maintained in the accessor.")]
    T value;

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

    public void Set(T value)
    {
        this.value = value;
        OnValueSet();
        //Debug.Log(name + " SingleAccessor Set value: " + value);
    }

    public T Get()
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