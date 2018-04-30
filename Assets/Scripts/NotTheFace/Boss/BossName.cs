// Author(s): Paul Calande
// Randomized boss name for Not the Face.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossName : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The text to use for the boss name.")]
    Text text;
    [SerializeField]
    [Tooltip("Stuff that the world eater eats.")]
    SOAString foodStrings;

    private void Start()
    {
        string food = foodStrings.GetRandomElement();
        text.text = UtilRandom.Spam(8, false) + " THE " + food + " EATER";
    }
}