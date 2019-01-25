// Author(s): Paul Calande
// Updates a box collider's size based on a sprite's dimensions.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAccessorToBoxCollider2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The accessor to the sprite to use for collider dimensions.")]
    SpriteAccessor accessor;
    [SerializeField]
    [Tooltip("The collider to adjust based on the sprite dimensions.")]
    BoxCollider2D collide;
    [SerializeField]
    [Tooltip("The scale of the collider size relative to the sprite size.")]
    float scale = 1.0f;

    private void Start()
    {
        accessor.Subscribe(SpriteAccessor_SpriteChanged);
    }

    private void SpriteAccessor_SpriteChanged(Sprite sprite)
    {
        collide.size = sprite.bounds.size * scale;
    }
}