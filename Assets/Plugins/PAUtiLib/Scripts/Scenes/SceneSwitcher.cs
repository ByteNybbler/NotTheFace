// Author(s): Paul Calande
// Switches scenes.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The new scene to switch to.")]
    SceneField newScene;

    public void Fire()
    {
        ServiceLocator.GetSceneTracker().SwitchScene(newScene);
    }
}