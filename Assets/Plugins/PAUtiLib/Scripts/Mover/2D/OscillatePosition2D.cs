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
        [Tooltip("The size of the x oscillation.")]
        public float xMagnitude;
        [Tooltip("The speed of the x oscillation.")]
        public float xSpeed;
        [Tooltip("The initial half turns value of the x oscillation.")]
        public float xStartingHalfTurns;
        [Tooltip("The size of the y oscillation.")]
        public float yMagnitude;
        [Tooltip("The speed of the y oscillation.")]
        public float ySpeed;
        [Tooltip("The initial half turns value of the y oscillation.")]
        public float yStartingHalfTurns;

        public Data(float xMagnitude, float xSpeed,
            float yMagnitude, float ySpeed,
            float xStartingHalfTurns = 0.0f, float yStartingHalfTurns = 0.0f)
        {
            this.xMagnitude = xMagnitude;
            this.xSpeed = xSpeed;
            this.yMagnitude = yMagnitude;
            this.ySpeed = ySpeed;
            this.xStartingHalfTurns = xStartingHalfTurns;
            this.yStartingHalfTurns = yStartingHalfTurns;
        }

        public Data DeepCopy()
        {
            return new Data(xMagnitude, xSpeed, yMagnitude, ySpeed,
                xStartingHalfTurns, yStartingHalfTurns);
        }
    }
    [SerializeField]
    Data data;

    [SerializeField]
    TimeScale timeScale;
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
        oscX = new Oscillator(data.xMagnitude, data.xSpeed, Mathf.Sin, data.xStartingHalfTurns);
        oscY = new Oscillator(data.yMagnitude, data.ySpeed, Mathf.Sin, data.yStartingHalfTurns);
    }

    private void FixedUpdate()
    {
        float xDifference = oscX.SampleDelta(timeScale.DeltaTime());
        float yDifference = oscY.SampleDelta(timeScale.DeltaTime());
        Vector2 change = new Vector2(xDifference, yDifference);
        mover.OffsetPosition(change);
    }
}