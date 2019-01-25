// Author(s): Paul Calande
// Displays the observed time rate for a given TimeObservable.
// This is distinct from TextTimeDilation, which returns the raw time rate.

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
        source.TimeRateChanged += SetRate;
        SetRate(1.0f);
    }

    private void SetRate(float newRate)
    {
        text.text = "OBSERVED TIME RATE: x" + newRate;
    }
}