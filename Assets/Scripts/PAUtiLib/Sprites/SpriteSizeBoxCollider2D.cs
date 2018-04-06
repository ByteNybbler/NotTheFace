// Author(s): Paul Calande
// Updates a box collider's size based on a sprite's dimensions.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSizeBoxCollider2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The accessor to the sprite to use for collider dimensions.")]
    SpriteAccessor accessor;
    [SerializeField]
    [Tooltip("The collider to adjust based on the sprite dimensions.")]
    BoxCollider2D collide;

    private void Start()
    {
        accessor.SpriteChanged += SpriteAccessor_SpriteChanged;
        SpriteAccessor_SpriteChanged(accessor.GetSprite());
    }

    private void SpriteAccessor_SpriteChanged(Sprite sprite)
    {
        collide.size = sprite.bounds.size;
    }
}