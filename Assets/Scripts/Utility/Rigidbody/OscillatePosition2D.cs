// Author(s): Paul Calande
// Modify a GameObject's position based on sinusoidal curves.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillatePosition2D : MonoBehaviour
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
        [Tooltip("The size of the x oscillation.")]
        public float xMagnitude;
        [Tooltip("The speed of the x oscillation.")]
        public float xSpeed;
        [Tooltip("The size of the y oscillation.")]
        public float yMagnitude;
        [Tooltip("The speed of the y oscillation.")]
        public float ySpeed;

        public Data(Refs refs, float xMagnitude, float xSpeed,
            float yMagnitude, float ySpeed)
        {
            this.refs = refs;
            this.xMagnitude = xMagnitude;
            this.xSpeed = xSpeed;
            this.yMagnitude = yMagnitude;
            this.ySpeed = ySpeed;
        }

        public Data DeepCopy()
        {
            return new Data(refs, xMagnitude, xSpeed, yMagnitude, ySpeed);
        }
    }
    [SerializeField]
    Data data;

    [SerializeField]
    [Tooltip("Reference to the Mover component.")]
    Mover2D mover;

    Oscillator oscX;
    Oscillator oscY;

    public void SetData(Data val)
    {
        data = val;
    }

    private void Start()
    {
        oscX = new Oscillator(data.xMagnitude, data.xSpeed, Mathf.Sin);
        oscY = new Oscillator(data.yMagnitude, data.ySpeed, Mathf.Sin);
    }

    private void FixedUpdate()
    {
        float xDifference = oscX.SampleDelta(data.refs.ts.DeltaTime());
        float yDifference = oscY.SampleDelta(data.refs.ts.DeltaTime());
        Vector2 change = new Vector2(xDifference, yDifference);
        mover.OffsetPosition(change);
    }
}