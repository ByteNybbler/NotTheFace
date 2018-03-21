// Author(s): Paul Calande
// Modulo-based positioning, for repeating patterns.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModularPosition2D : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        [System.Serializable]
        public class Refs
        {
            [Tooltip("Reference to the mover component.")]
            public Mover2D mover;

            public Refs(Mover2D mover)
            {
                this.mover = mover;
            }
        }
        public Refs refs;
        [Tooltip("The region to wrap around in.")]
        public Bounds bounds;

        public Data(Refs refs, Bounds bounds)
        {
            this.refs = refs;
            this.bounds = bounds;
        }
    }
    [SerializeField]
    Data data;

    public void SetData(Data val)
    {
        data = val;
    }

    private void FixedUpdate()
    {
        Vector2 futurePos = data.refs.mover.GetPosition();
        while (futurePos.x < data.bounds.min.x)
        {
            futurePos += new Vector2(data.bounds.size.x, 0.0f);
        }
        while (futurePos.y < data.bounds.min.y)
        {
            futurePos += new Vector2(0.0f, data.bounds.size.y);
        }
        while (futurePos.x > data.bounds.max.x)
        {
            futurePos -= new Vector2(data.bounds.size.x, 0.0f);
        }
        while (futurePos.y > data.bounds.max.y)
        {
            futurePos -= new Vector2(0.0f, data.bounds.size.y);
        }
        data.refs.mover.TeleportPosition(futurePos);

        /*
        Vector2 currentPos = data.refs.mover.GetPosition();
        Debug.Log("bounds / currentPos / data.bounds.size.x: "
            + data.bounds + " / "
            + currentPos + " / "
            + data.bounds.size.x);
            */
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(data.bounds.center, data.bounds.size);
    }
    */
}