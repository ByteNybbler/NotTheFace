// Author(s): Paul Calande
// Plays a random sound effect when the GameObject is disabled.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDisableToRandomSFX : RandomSFX
{
    private void OnDisable()
    {
        Fire();
    }
}