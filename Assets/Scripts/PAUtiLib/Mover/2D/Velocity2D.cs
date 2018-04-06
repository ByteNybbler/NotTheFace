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
        [Tooltip("The vector determining the direction and speed to move.")]
        public Vector2 velocity;

        public Data(Vector2 velocity)
        {
            this.velocity = velocity;
        }

        public Data DeepCopy()
        {
            return new Data(velocity);
        }
    }
    [SerializeField]
    Data data;
    [System.Serializable]
    public class Refs
    {
        public TimeScale timeScale;

        public Refs(TimeScale timeScale)
        {
            this.timeScale = timeScale;
        }
    }
    [SerializeField]
    Refs refs;

    [SerializeField]
    [Tooltip("Reference to the Mover component.")]
    Mover2D mover;

    public void SetData(Data val)
    {
        data = val;
    }
    public void SetRefs(Refs val)
    {
        refs = val;
    }

    private void FixedUpdate()
    {
        mover.OffsetPosition(data.velocity * refs.timeScale.DeltaTime());
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