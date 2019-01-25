// Author(s): Paul Calande
// Component with a public method that quits the game.
// Useful for UI buttons that quit the game.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameQuitter : MonoBehaviour
{
    public void Quit()
    {
        UtilScene.ExitGame();
    }
}