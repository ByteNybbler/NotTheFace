// Author(s): Paul Calande
// Makes an object lerp to a series of positions in succession.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpToPositions2D : MonoBehaviour
{
    // Invoked when the object finishes lerping to the final position.
    public delegate void CompletedHandler();
    public event CompletedHandler Completed;

    // In this case, a node is a position associated with some form of speed.
    public class Node
    {
        public enum LerpType
        {
            Seconds,
            Speed
        }

        public Vector2 position;
        public LerpType lerpType;
        public float lerpQuantity;

        public Node(Vector2 position, LerpType lerpType, float lerpQuantity)
        {
            this.position = position;
            this.lerpType = lerpType;
            this.lerpQuantity = lerpQuantity;
        }
    }

    [SerializeField]
    [Tooltip("The component to use to lerp the object.")]
    LerpToPosition2D lerper;
    [SerializeField]
    [Tooltip("The nodes to lerp to.")]
    Node[] nodes;

    // The current target index that the object is trying to head towards.
    int targetIndex = 0;

    // Start moving along the nodes.
    public void BeginWithFirstNode()
    {
        SetTargetIndex(0);
    }
    
    private void SetTargetIndexToCurrent()
    {
        SetTargetIndex(targetIndex);
    }

    public void SetTargetIndex(int index)
    {
        lerper.Completed -= SetTargetIndexToCurrent;
        Node node = nodes[index];
        if (node.lerpType == Node.LerpType.Seconds)
        {
            lerper.LerpToInTime(node.position, node.lerpQuantity);
        }
        else
        {
            lerper.LerpToAtSpeed(node.position, node.lerpQuantity);
        }
        targetIndex = index + 1;
        if (targetIndex == nodes.Length)
        {
            lerper.Completed += OnCompleted;
        }
        else
        {
            lerper.Completed += SetTargetIndexToCurrent;
        }
    }

    public void SetNodes(Node[] nodes)
    {
        this.nodes = nodes;
    }

    private void OnCompleted()
    {
        if (Completed != null)
        {
            Completed();
        }
    }
}