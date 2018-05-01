// Author(s): Paul Calande
// Script for loading the initial scenes required to run the game.
// Also loads the scene in which the actual game starts.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The names of the first scenes to load.")]
    SceneField[] permanentScenes;
    [SerializeField]
    [Tooltip("The first temporary scene to load: essentially the game startup.")]
    SceneField firstTemporaryScene;

    private void Awake()
    {
        for (int i = 0; i < permanentScenes.Length; ++i)
        {
            SceneManager.LoadScene(permanentScenes[i], LoadSceneMode.Additive);
        }
    }

    private void Start()
    {
        SceneTracker st = ServiceLocator.GetSceneTracker();
        st.SwitchScene(firstTemporaryScene);
    }
}