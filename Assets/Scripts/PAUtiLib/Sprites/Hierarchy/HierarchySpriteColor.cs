// Author(s): Paul Calande
// Propagates a sprite color down the GameObject hierarchy.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HierarchySpriteColor : MonoBehaviour
{
    public void SetColor(Color col)
    {
        SpriteAccessor[] accessors = GetComponentsInChildren<SpriteAccessor>();
        foreach (SpriteAccessor accessor in accessors)
        {
            accessor.SetColor(col);
        }
    }
}