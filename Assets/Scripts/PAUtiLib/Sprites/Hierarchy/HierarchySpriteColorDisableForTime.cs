// Author(s): Paul Calande
// Changes hierarchy sprite color when DisableForTime either enables or disables
// its GameObject.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HierarchySpriteColorDisableForTime : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The component to use to color the sprite.")]
    HierarchySpriteColor hierarchySpriteColor;
    [SerializeField]
    [Tooltip("The enabler/disabler to subscribe to.")]
    DisableForTime disableForTime;
    [SerializeField]
    [Tooltip("The color to use when the GameObject is disabled.")]
    Color colorDisabled = Color.red;

    Color colorNormal = Color.white;

    private void Start()
    {
        disableForTime.StateChanged += DisableForTime_StateChanged;
    }

    private void DisableForTime_StateChanged(bool active)
    {
        if (active)
        {
            SetColor(colorNormal);
        }
        else
        {
            SetColor(colorDisabled);
        }
    }

    private void SetColor(Color color)
    {
        hierarchySpriteColor.SetColor(color);
    }
}