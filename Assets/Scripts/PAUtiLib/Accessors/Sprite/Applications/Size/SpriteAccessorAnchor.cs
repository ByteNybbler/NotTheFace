// Author(s): Paul Calande
// Effectively keeps the given edge of a sprite at the given coordinate.
// Accomplished by changing this GameObject's transform position.
// Note that this only really seems to work for sprites that have pivots at their centers.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAccessorAnchor : MonoBehaviour
{
    public enum Anchor
    {
        Left,
        Right,
        Top,
        Bottom
    }

    [SerializeField]
    [Tooltip("Subscribes to the accessor to the SpriteRenderer to anchor.")]
    SpriteAccessor accessor;
    [SerializeField]
    [Tooltip("The anchor to use.")]
    Anchor anchor;
    [SerializeField]
    [Tooltip("The anchor's coordinate.")]
    float coordinate;

    private void Start()
    {
        accessor.Subscribe(SpriteUpdate);
    }

    private void UpdateBasedOnCurrentSprite()
    {
        SpriteUpdate(accessor.Get());
    }

    public void SetCoordinate(float coordinate)
    {
        this.coordinate = coordinate;
        UpdateBasedOnCurrentSprite();
    }

    // Gets the dimension of the given sprite on the anchor axis.
    private float GetSizeTowardsAnchor(Sprite sprite)
    {
        switch (anchor)
        {
            case Anchor.Left:
            case Anchor.Right:
                return sprite.bounds.size.x;
            case Anchor.Top:
            case Anchor.Bottom:
            default:
                return sprite.bounds.size.y;
        }
    }

    private void SpriteUpdate(Sprite sprite)
    {
        float offset = GetSizeTowardsAnchor(sprite) * 0.5f;
        Vector3 newPos = transform.position;

        switch (anchor)
        {
            case Anchor.Left:
                newPos.x = coordinate + offset;
                break;
            case Anchor.Right:
                newPos.x = coordinate - offset;
                break;
            case Anchor.Top:
                newPos.y = coordinate - offset;
                break;
            case Anchor.Bottom:
                newPos.y = coordinate + offset;
                break;
        }

        transform.position = newPos;
    }
}