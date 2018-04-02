// Author(s): Paul Calande
// Script for duplicating a tile to form a grid.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("How many tiles to have along the x-axis.")]
    int xTiles;
    [SerializeField]
    [Tooltip("How many tiles to have along the y-axis.")]
    int yTiles;
    [SerializeField]
    [Tooltip("Whether to add modular positioning components to the instantiated tiles. "
        + "Does not add the other necessary components like mover, Rigidbody, etc.")]
    bool addPositionModulo;
    [SerializeField]
    [Tooltip("Whether to mirror every other tile horizontally.")]
    bool alternatingMirrorHorizontal;
    [SerializeField]
    [Tooltip("Whether to mirror every other tile vertically.")]
    bool alternatingMirrorVertical;
    [SerializeField]
    [Tooltip("Reference to the base tile's renderer component. The GameObject that owns "
        + "the renderer component will be copied and pasted to form the grid of tiles.")]
    SpriteRenderer tileRenderer;

    private void Start()
    {
        // The width of one tile.
        float width = tileRenderer.bounds.size.x;
        // The height of one tile.
        float height = tileRenderer.bounds.size.y;
        // The width of all tiles combined.
        float totalWidth = width * xTiles;
        // The height of all tiles combined.
        float totalHeight = height * yTiles;

        Bounds region = new Bounds(tileRenderer.transform.position, new Vector2(totalWidth, totalHeight));

        Vector3 xOffset = new Vector2(width, 0.0f);
        Vector3 yOffset = new Vector2(0.0f, height);

        // The base position to use in the tile-generating loop.
        Vector3 basePosition = new Vector3(
            (-totalWidth + width) * 0.5f,
            (-totalHeight + height) * 0.5f,
            0.0f);

        for (int x = 0; x < xTiles; ++x)
        {
            for (int y = 0; y < yTiles; ++y)
            {
                //GameObject tile = new GameObject();
                GameObject tile = Instantiate(tileRenderer.gameObject);
                tile.transform.parent = tileRenderer.transform.parent;
                tile.transform.position = basePosition + xOffset * x + yOffset * y;
                Vector3 scale = tileRenderer.transform.localScale;
                if (alternatingMirrorHorizontal)
                {
                    if (x % 2 == 0)
                    {
                        scale.x *= -1;
                    }
                }
                if (alternatingMirrorVertical)
                {
                    if (y % 2 == 0)
                    {
                        scale.y *= -1;
                    }
                }
                tile.transform.localScale = scale;
                if (addPositionModulo)
                {
                    ModularPosition2D pm = tile.AddComponent<ModularPosition2D>();
                    ModularPosition2D.Data pmData = new ModularPosition2D.Data(
                        new ModularPosition2D.Data.Refs(tile.GetComponent<Mover2D>()),
                        region);
                    pm.SetData(pmData);
                }
            }
        }

        // Destroy the base tile now that we're done with it.
        Destroy(tileRenderer.gameObject);
    }
}