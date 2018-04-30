// Author(s): Invertex (via Unity Answers)
// https://forum.unity.com/threads/collision-with-sides-of-screen.228865/
// Generates colliders on the edges of a Camera view.
// Targets the main camera in this case.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEdgeColliders : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The thickness of the colliders.")]
    float thickness = 4f;
    [SerializeField]
    [Tooltip("The z position of the camera edge.")]
    float zPosition = 0f;

    void Start()
    {
        // Generate empty objects to contain the colliders.
        Transform topCollider = new GameObject().transform;
        Transform bottomCollider = new GameObject().transform;
        Transform rightCollider = new GameObject().transform;
        Transform leftCollider = new GameObject().transform;

        // Name our objects.
        topCollider.name = "TopEdgeCollider";
        bottomCollider.name = "BottomEdgeCollider";
        rightCollider.name = "RightEdgeCollider";
        leftCollider.name = "LeftEdgeCollider";

        // Add the collider components.
        topCollider.gameObject.AddComponent<BoxCollider2D>();
        bottomCollider.gameObject.AddComponent<BoxCollider2D>();
        rightCollider.gameObject.AddComponent<BoxCollider2D>();
        leftCollider.gameObject.AddComponent<BoxCollider2D>();

        // Make the collider GameObjects children of the camera so that they will move with the camera.
        Transform cameraTransform = Camera.main.transform;
        topCollider.parent = cameraTransform;
        bottomCollider.parent = cameraTransform;
        rightCollider.parent = cameraTransform;
        leftCollider.parent = cameraTransform;

        // Generate world space point information for position and scale calculations.
        Vector3 cameraPos = Camera.main.transform.position;
        Vector2 screenBottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 screenBottomRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0));
        Vector2 screenTopLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));
        Vector2 halfScreenSize;
        halfScreenSize.x = (screenBottomRight - screenBottomLeft).x * 0.5f;
        halfScreenSize.y = (screenTopLeft - screenBottomLeft).y * 0.5f;

        // Change our scale and positions to match the edges of the screen...   
        rightCollider.localScale = new Vector3(thickness, halfScreenSize.y * 2, thickness);
        rightCollider.position = new Vector3(cameraPos.x + halfScreenSize.x + (rightCollider.localScale.x * 0.5f), cameraPos.y, zPosition);
        leftCollider.localScale = new Vector3(thickness, halfScreenSize.y * 2, thickness);
        leftCollider.position = new Vector3(cameraPos.x - halfScreenSize.x - (leftCollider.localScale.x * 0.5f), cameraPos.y, zPosition);
        topCollider.localScale = new Vector3(halfScreenSize.x * 2, thickness, thickness);
        topCollider.position = new Vector3(cameraPos.x, cameraPos.y + halfScreenSize.y + (topCollider.localScale.y * 0.5f), zPosition);
        bottomCollider.localScale = new Vector3(halfScreenSize.x * 2, thickness, thickness);
        bottomCollider.position = new Vector3(cameraPos.x, cameraPos.y - halfScreenSize.y - (bottomCollider.localScale.y * 0.5f), zPosition);
    }
}