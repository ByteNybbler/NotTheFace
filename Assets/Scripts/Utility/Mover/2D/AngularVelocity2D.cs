// Author(s): Paul Calande
// Continuously rotates the GameObject at a constant rate.
// Has the advantage of being suspectible to the effects of time scales.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngularVelocity2D : MonoBehaviour
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
        [Tooltip("How quickly to rotate.")]
        public float angularVelocity;

        public Data(Refs refs, float angularVelocity)
        {
            this.refs = refs;
            this.angularVelocity = angularVelocity;
        }

        public Data DeepCopy()
        {
            return new Data(refs, angularVelocity);
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
        mover.OffsetRotation(data.angularVelocity * data.refs.ts.DeltaTime());
    }

    public void SetAngularVelocity(float val)
    {
        data.angularVelocity = val;
    }

    public float GetAngularVelocity()
    {
        return data.angularVelocity;
    }
}