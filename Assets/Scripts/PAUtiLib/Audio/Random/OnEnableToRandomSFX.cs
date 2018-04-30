// Author(s): Paul Calande
// Plays a random sound effect when the GameObject is enabled.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableToRandomSFX : RandomSFX
{
    private void OnEnable()
    {
        Fire();
    }
}