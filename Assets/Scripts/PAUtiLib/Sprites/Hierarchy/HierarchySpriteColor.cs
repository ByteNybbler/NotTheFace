// Author(s): Paul Calande
// Propagates a sprite color down the GameObject hierarchy.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HierarchySpriteColor : MonoBehaviour
{
    SpriteAccessor[] accessors;

    private void Awake()
    {
        accessors = GetComponentsInChildren<SpriteAccessor>();
    }

    public void SetColor(Color col)
    {
        foreach (SpriteAccessor accessor in accessors)
        {
            accessor.SetColor(col);
        }
    }
}