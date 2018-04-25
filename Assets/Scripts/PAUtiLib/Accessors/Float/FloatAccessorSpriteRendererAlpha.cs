// Author(s): Paul Calande
// FloatAccessor support for SpriteRenderer component color alpha.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatAccessorSpriteRendererAlpha : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The accessor to connect to.")]
    FloatAccessor accessor;
    [SerializeField]
    [Tooltip("The SpriteRenderer to access the alpha of.")]
    SpriteRenderer render;
    [SerializeField]
    [Tooltip("How much to scale the alpha by.")]
    float alphaScale = 1.0f;

    private void Start()
    {
        accessor.Subscribe(SetAlpha);
    }

    private void SetAlpha(float alpha)
    {
        render.color = UtilColor.SetAlpha(render.color, alpha * alphaScale);
    }
}