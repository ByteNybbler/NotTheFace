// Author(s): Paul Calande
// FloatAccessor support for changing the modifier value of a modifier region.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TRegion is the class of the region.
public class FloatAccessorModifierRegion2D<TMonoIndexedList, TRegion> : MonoBehaviour
    where TMonoIndexedList : FloatProductIndexedList
    where TRegion : ModifierRegion2D<float, TMonoIndexedList>
{
    [SerializeField]
    [Tooltip("The accessor to connect to.")]
    FloatAccessor accessor;
    [SerializeField]
    [Tooltip("The region to change the modifier of.")]
    TRegion region;

    private void Start()
    {
        accessor.Subscribe(region.SetModifier);
    }
}