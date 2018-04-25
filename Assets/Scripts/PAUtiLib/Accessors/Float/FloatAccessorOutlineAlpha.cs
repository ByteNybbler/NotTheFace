// Author(s): Paul Calande
// FloatAccessor support for Outline component color alpha.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatAccessorOutlineAlpha : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The accessor to connect to.")]
    FloatAccessor accessor;
    [SerializeField]
    [Tooltip("The outline to access the alpha of.")]
    Outline outline;
    [SerializeField]
    [Tooltip("How much to scale the alpha by.")]
    float alphaScale = 1.0f;

    private void Start()
    {
        accessor.Subscribe(SetAlpha);
    }

    private void SetAlpha(float alpha)
    {
        outline.effectColor = UtilColor.SetAlpha(outline.effectColor, alpha * alphaScale);
    }
}