// Author(s): Paul Calande
// Continuously moves the GameObject based on a given movement vector.
// Has the advantage of being suspectible to the effects of time scales.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocity2D : MonoBehaviour
{
    [System.Serializable]
    public class Data : IDeepCopyable<Data>
    {
        [System.Serializable]
        public class Refs
        {
            public TimeScale ts;

            public Refs(TimeScale ts)
            {
                this.ts = ts;
            }
        }
        public Refs refs;
        [Tooltip("The vector determining the direction and speed to move.")]
        public Vector2 velocity;

        public Data(Refs refs, Vector2 velocity)
        {
            this.refs = refs;
            this.velocity = velocity;
        }

        public Data DeepCopy()
        {
            return new Data(refs, velocity);
        }
    }
    [SerializeField]
    Data data;

    [SerializeField]
    [Tooltip("Reference to the Mover component.")]
    Mover2D mover;

    public void SetData(Data val)
    {
        data = val;
    }

    private void FixedUpdate()
    {
        mover.OffsetPosition(data.velocity * data.refs.ts.DeltaTime());
    }

    public void SetVelocity(Vector2 val)
    {
        data.velocity = val;
    }

    public Vector2 GetVelocity()
    {
        return data.velocity;
    }
}