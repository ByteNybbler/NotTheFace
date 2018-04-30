// Author(s): Paul Calande
// SpriteAccessor support for SpriteRenderer component.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAccessorSpriteRenderer : SingleAccessorConnection
    <Sprite, SpriteAccessor, SpriteRenderer>
{    
    protected override void Set(Sprite sprite)
    {
        connected.sprite = sprite;
    }

    protected override Sprite Get()
    {
        return connected.sprite;
    }
}