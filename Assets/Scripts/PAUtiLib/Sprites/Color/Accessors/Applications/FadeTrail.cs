// Author(s): Paul Calande
// Leaves behind a fading trail of the given prefab.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTrail : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The GameObject to use to make the trail.")]
    GameObject prefabTrail;
    [SerializeField]
    [Tooltip("The Transform to use as a reference for the trail's transform.")]
    Transform trans;
    [SerializeField]
    [Tooltip("How many seconds one instance of the trail takes to fade.")]
    float secondsToFade;

    private void FixedUpdate()
    {
        GameObject fader = Instantiate(prefabTrail);
        fader.transform.position = trans.position;
        fader.transform.rotation = trans.rotation;
        fader.transform.localScale = trans.lossyScale;
        ColorFadeDestroy fade = fader.GetComponent<ColorFadeDestroy>();
        fade.Fade(secondsToFade);
    }
}