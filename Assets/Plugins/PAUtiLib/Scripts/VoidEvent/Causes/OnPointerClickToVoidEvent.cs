// Author(s): Paul Calande
// Invokes a VoidEvent when the object is clicked.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnPointerClickToVoidEvent : VoidEvent, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        OnFired();
    }
}