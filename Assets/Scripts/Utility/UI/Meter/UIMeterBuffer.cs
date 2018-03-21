// Author(s): Paul Calande
// Script that controls a buffered layer of a meter.
// The yellow part of the Cave Story boss health meter is a good example of this.
// Here's the layout of a typical buffered health bar (from top to bottom):
// 1. Top buffer bar (healing).
// 2. Main (actual) health bar.
// 3. Bottom buffer bar (damage taken).
// 4. Back bar (where no other bar layers are present).

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMeterBuffer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The UIMeter to track.")]
    UIMeter meterMain;
    [SerializeField]
    [Tooltip("The UIMeter to use as the buffer.")]
    UIMeter meterBuffer;
    [SerializeField]
    TimeScale ts;
    [SerializeField]
    [Tooltip("Whether the buffered meter shrinks towards the right or the left.")]
    bool shrinkToLeft;
    [SerializeField]
    [Tooltip("Seconds to hesitate before the buffering begins.")]
    float secondsToHesitate;
    [SerializeField]
    [Tooltip("How quickly the buffer shrinks.")]
    float shrinkSpeed;

    // The percentage for the lower end of the buffer.
    float bufferLower = 0.0f;
    // The percentage for the upper end of the buffer.
    float bufferUpper = 0.0f;
    // The timer for the buffer hesitating before it begins to move.
    Timer timerHesitate;
    // Whether the hesitation timer has finished.
    bool timerFinished = false;

    private void Start()
    {
        meterMain.PercentChanged += UIMeter_OnPercentChanged;
        timerHesitate = new Timer(secondsToHesitate, false, false);
        UpdateBufferMeter();
    }

    // Updates the appearance of the buffer meter.
    private void UpdateBufferMeter()
    {
        //Debug.Log(name + " UpdateBufferMeter");
        meterBuffer.SetLeftAnchor(bufferLower);
        meterBuffer.SetPercent(bufferUpper);
    }

    private bool IsTimerReady()
    {
        return !timerFinished && !timerHesitate.IsRunning();
    }

    // Callback function for when the main meter changes.
    private void UIMeter_OnPercentChanged(float percentOld, float percentNew)
    {
        if (shrinkToLeft)
        {
            if (percentNew < percentOld)
            {
                bufferLower = percentNew;
                if (IsTimerReady())
                {
                    bufferUpper = percentOld;
                    timerHesitate.Start();
                }
            }
            else if (!IsTimerReady())
            {
                if (percentNew >= bufferUpper)
                {
                    timerHesitate.Stop();
                    timerHesitate.Reset();
                    bufferLower = 0.0f;
                    bufferUpper = 0.0f;
                }
                else
                {
                    bufferLower = percentNew;
                }
            }
        }
        else
        {
            if (percentNew > percentOld)
            {
                bufferUpper = percentNew;
                if (IsTimerReady())
                {
                    bufferLower = percentOld;
                    timerHesitate.Start();
                }
            }
            else if (!IsTimerReady())
            {
                if (percentNew <= bufferLower)
                {
                    timerHesitate.Stop();
                    timerHesitate.Reset();
                    bufferLower = 0.0f;
                    bufferUpper = 0.0f;
                }
                else
                {
                    bufferUpper = percentNew;
                }
            }
        }

        UpdateBufferMeter();
    }

    private void FixedUpdate()
    {
        float dt = ts.DeltaTime();
        while (timerHesitate.TimeUp(dt))
        {
            timerFinished = true;
        }
        if (timerFinished)
        {
            // Shrink the buffer.
            float stepSize = dt * shrinkSpeed;
            if (shrinkToLeft)
            {
                bufferUpper = UtilMath.Approach(bufferUpper, bufferLower, stepSize);
            }
            else
            {
                bufferLower = UtilMath.Approach(bufferLower, bufferUpper, stepSize);
            }
            UpdateBufferMeter();

            if (bufferLower == bufferUpper)
            {
                timerFinished = false;
            }
        }
    }
}