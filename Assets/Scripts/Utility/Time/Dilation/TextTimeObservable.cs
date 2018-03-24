// Author(s): Paul Calande
// Displays the time rate for a given TimeObservable.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTimeObservable : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The text to display the time rate in.")]
    Text text;
    [SerializeField]
    [Tooltip("The TimeObservable to retrieve the time rate from.")]
    TimeObservable source;

    private void Start()
    {
        SetTimeRate(1.0f);
        source.TimeRateChanged += TimeObservable_TimeRateChanged;
    }

    private void SetTimeRate(float newTimeRate)
    {
        text.text = "x" + newTimeRate;
    }

    private void TimeObservable_TimeRateChanged(float newTimeRate)
    {
        SetTimeRate(newTimeRate);
    }
}