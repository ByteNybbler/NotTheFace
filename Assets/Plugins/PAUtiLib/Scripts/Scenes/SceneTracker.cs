// Author(s): Paul Calande
// Script for managing temporary scenes.
// Permanent scenes are loaded by the GameStarter class.
// Full scene paths ("Assets/.../.../Scene.unity") should be passed as arguments to
// the methods of this class. For this, SceneField is useful.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTracker : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The name of the loading screen scene.")]
    SceneField sceneLoading;

    // The name of the current scene.
    string sceneCurrent;

    private string GetActiveScene()
    {
        return SceneManager.GetActiveScene().path;
    }

    // Switch to a new temporary scene, unloading the old one.
    public void SwitchScene(string newScenePath)
    {
        string scenePrevious = sceneCurrent;
        sceneCurrent = newScenePath;
        SceneManager.LoadScene(sceneLoading, LoadSceneMode.Additive);
        if (scenePrevious == null)
        {
            // If no previous scene exists, just use the active scene.
            scenePrevious = GetActiveScene();
        }
        SceneManager.UnloadSceneAsync(scenePrevious).completed += UnloadFinished;
    }

    private void UnloadFinished(AsyncOperation async)
    {
        LoadScene(sceneCurrent);
    }

    private void LoadScene(string newScenePath)
    {
        SceneManager.LoadSceneAsync(newScenePath, LoadSceneMode.Additive).completed += LoadFinished;
    }

    private void LoadFinished(AsyncOperation async)
    {
        SceneManager.UnloadSceneAsync(sceneLoading);
        SceneManager.SetActiveScene(SceneManager.GetSceneByPath(sceneCurrent));
    }
    
    // Restarts the current temporary scene.
    public void RestartScene()
    {
        if (sceneCurrent == null)
        {
            sceneCurrent = GetActiveScene();
        }
        SwitchScene(sceneCurrent);
    }
}