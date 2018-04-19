// Author(s): Paul Calande
// SpriteAccessor support for SpriteRenderer component.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpriteAccessorSpriteRenderer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The accessor to connect to.")]
    SpriteAccessor accessor;
    [SerializeField]
    [Tooltip("The SpriteRenderer to access the sprite of.")]
    SpriteRenderer render;
    
    private void Start()
    {
        accessor.Subscribe(SetSprite);
    }

    private void SetSprite(Sprite sprite)
    {
        render.sprite = sprite;
        //Debug.Log(name + " SpriteAccessorSpriteRenderer: Set sprite to " + sprite);
    }
}