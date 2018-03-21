// Author(s): Paul Calande
// Propagates a sprite color down the GameObject hierarchy.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HierarchySpriteColor : MonoBehaviour
{
    SpriteRenderer[] renderers;

    private void Awake()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    public void SetColor(Color col)
    {
        foreach (SpriteRenderer render in renderers)
        {
            render.color = col;
        }
    }
}