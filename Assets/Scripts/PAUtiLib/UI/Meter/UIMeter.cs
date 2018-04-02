// Author(s): Paul Calande
// Class for manipulating UI meters.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMeter : MonoBehaviour
{
    // Invoked when the meter's percentage is changed.
    public delegate void PercentChangedHandler(float percentOld, float percentNew);
    public event PercentChangedHandler PercentChanged;

    [SerializeField]
    [Tooltip("Reference to the RectTransform of the front component of the meter.")]
    RectTransform meterFront;

    // Fills the meter up to the given percentage.
    public void SetPercent(float percent)
    {
        float oldPercent = meterFront.anchorMax.x;

        // Cap the percent at 100% so that the anchors don't overflow.
        if (percent > 1.0f)
        {
            percent = 1.0f;
        }

        // Adjust anchors.
        Vector2 newAnchorMax = meterFront.anchorMax;
        newAnchorMax.x = percent;
        meterFront.anchorMax = newAnchorMax;

        // After the anchors are adjusted, invoke the percent changed event.
        OnPercentChanged(oldPercent, percent);
    }

    // Fills the meter up to the calculated percentage based on the given proportion.
    public void SetProportion(float currentValue, float maxValue)
    {
        SetPercent(currentValue / maxValue);
    }

    // Returns the current percent of the meter.
    public float GetCurrentPercent()
    {
        return meterFront.anchorMax.x;
    }

    // Adjusts the leftmost anchor of the meter.
    // Mainly useful if this meter is a buffer meter.
    public void SetLeftAnchor(float percent)
    {
        Vector2 newAnchorMin = meterFront.anchorMin;
        newAnchorMin.x = percent;
        meterFront.anchorMin = newAnchorMin;
    }

    private void OnPercentChanged(float percentOld, float percentNew)
    {
        if (PercentChanged != null)
        {
            PercentChanged(percentOld, percentNew);
        }
    }
}