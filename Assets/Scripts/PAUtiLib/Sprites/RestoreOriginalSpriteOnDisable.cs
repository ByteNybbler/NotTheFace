// Author(s): Paul Calande
// When this component is first activated, the current sprite in the given SpriteRenderer
// will be set as the "default sprite". The SpriteRenderer will be reassigned this sprite
// when this component is disabled. This effectively means that the SpriteRenderer will
// have the same sprite each time it is enabled (assuming the sprite doesn't change
// between the times of the disable and the enable).

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreOriginalSpriteOnDisable : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Accessor to the renderer to change the sprite of.")]
    SpriteAccessor accessor;

    Sprite defaultSprite;

    private void Start()
    {
        defaultSprite = accessor.GetSprite();
    }

    private void OnDisable()
    {
        accessor.SetSprite(defaultSprite);
    }
}