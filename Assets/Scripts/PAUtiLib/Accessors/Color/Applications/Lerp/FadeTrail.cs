// Author(s): Paul Calande
// Leaves behind a fading trail of the given prefab.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTrail : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The GameObject to use to make the trail. Needs a ColorFadeDestroy "
        + "component at its root.")]
    GameObject prefabTrail;
    [SerializeField]
    [Tooltip("The SpriteRenderer to use as a reference for making the trail.")]
    SpriteRenderer render;
    [SerializeField]
    [Tooltip("How many seconds one instance of the trail takes to fade.")]
    float secondsToFade;

    private void FixedUpdate()
    {
        GameObject fader = Instantiate(prefabTrail);
        fader.transform.position = render.transform.position;
        fader.transform.rotation = render.transform.rotation;
        fader.transform.localScale = render.transform.lossyScale;
        fader.GetComponent<SpriteRenderer>().sprite = render.sprite;
        ColorAccessorFadeDestroy fade = fader.GetComponent<ColorAccessorFadeDestroy>();
        fade.Fade(secondsToFade);
    }
}