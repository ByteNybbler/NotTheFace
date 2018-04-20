using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidEventToColorAccessorFade : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The VoidEvent to subscribe to.")]
    VoidEvent voidEvent;
    [SerializeField]
    [Tooltip("The fader to activate.")]
    ColorAccessorFade fader;

    private void Start()
    {
        voidEvent.Subscribe(fader.Fade);
    }
}