// Author(s): Paul Calande
// Player input script for Not the Face.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : InputDistributed
{
    [SerializeField]
    [Tooltip("Reference to the tongue component.")]
    PlayerTongue tongue;
    [SerializeField]
    [Tooltip("Used to make the player headbutt.")]
    PlayerHeadbutt headbutt;

    public override void ReceiveInput(InputReader inputReader)
    {
        if (inputReader.GetKeyDown(KeyCode.J))
        {
            tongue.Fire(false);
        }
        if (inputReader.GetKeyDown(KeyCode.K))
        {
            tongue.Fire(true);
        }
        if (inputReader.GetKeyDown(KeyCode.L))
        {
            if (!tongue.IsTongueRunning())
            {
                headbutt.HeadbuttEnter();
            }
        }
    }
}