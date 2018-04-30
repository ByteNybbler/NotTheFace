// Author(s): Paul Calande
// Script for managing temporary scenes.
// Permanent scenes are loaded by the GameStarter class.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTracker : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The name of the loading screen scene.")]
    string sceneLoading;

    // The name of the current scene.
    string sceneCurrent;

    // Switch to a new temporary scene, unloading the old one.
    public void SwitchScene(string newScene)
    {
        string scenePrevious = sceneCurrent;
        sceneCurrent = newScene;
        SceneManager.LoadScene(sceneLoading, LoadSceneMode.Additive);
        if (scenePrevious == null)
        {
            // If no previous scene exists, just use the scene that called this method.
            scenePrevious = SceneManager.GetActiveScene().name;
        }
        SceneManager.UnloadSceneAsync(scenePrevious).completed += UnloadFinished;
    }

    private void UnloadFinished(AsyncOperation async)
    {
        LoadScene(sceneCurrent);
    }

    private void LoadScene(string newScene)
    {
        SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive).completed += LoadFinished;
    }

    private void LoadFinished(AsyncOperation async)
    {
        SceneManager.UnloadSceneAsync(sceneLoading);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneCurrent));
    }
    
    // Restarts the current temporary scene.
    public void RestartScene()
    {
        if (sceneCurrent == null)
        {
            sceneCurrent = SceneManager.GetActiveScene().name;
        }
        SwitchScene(sceneCurrent);
    }
}