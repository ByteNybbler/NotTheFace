// Author(s): Paul Calande
// Rocket Puncher summary screen.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RPSummaryScreen : MonoBehaviour
{
    SceneTracker st;

    private void Awake()
    {
        st = ServiceLocator.GetSceneTracker();
    }

    public void PlayAgain()
    {
        st.RestartScene();
    }

    public void MainMenu()
    {
        st.SwitchScene("RPMainMenu");
    }
}