// Author(s): Paul Calande
// Not the Face player script.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    TextAsset filePlayer;
    [SerializeField]
    PlayerTongue playerTongue;

    private void Awake()
    {
        JSONNodeReader jsonP = new JSONNodeReader(filePlayer);
        playerTongue.SetData(new PlayerTongue.Data(
            jsonP.Get("seconds of tongue", 0.2f),
            jsonP.Get("seconds of tongue cooldown", 1.0f)));
    }
}