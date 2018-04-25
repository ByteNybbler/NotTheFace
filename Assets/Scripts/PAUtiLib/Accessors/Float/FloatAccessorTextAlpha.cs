// Author(s): Paul Calande
// FloatAccessor support for Text component color alpha.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatAccessorTextAlpha : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The accessor to connect to.")]
    FloatAccessor accessor;
    [SerializeField]
    [Tooltip("The text to access the alpha of.")]
    Text text;
    [SerializeField]
    [Tooltip("How much to scale the alpha by.")]
    float alphaScale = 1.0f;

    private void Start()
    {
        accessor.Subscribe(SetAlpha);
    }

    private void SetAlpha(float alpha)
    {
        text.color = UtilColor.SetAlpha(text.color, alpha * alphaScale);
    }
}