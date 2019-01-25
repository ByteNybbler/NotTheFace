// Author(s): Paul Calande
// Leaves behind a fading trail of the given sprite.

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
    [Tooltip("The accessor to fetch the sprite from and use for the fade trail.")]
    SpriteAccessor accessor;
    [SerializeField]
    [Tooltip("The transform to use as a reference for positioning and rotating the trail.")]
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
        fader.GetComponent<SpriteRenderer>().sprite = accessor.Get();
        MonoTimer fadeTimer = fader.GetComponent<MonoTimer>();
        fadeTimer.SetSecondsTarget(secondsToFade);
        fadeTimer.Run();
    }
}