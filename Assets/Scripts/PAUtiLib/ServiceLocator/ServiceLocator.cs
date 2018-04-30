// Author(s): Paul Calande
// Service locator class.
// Do not retrieve references to services in Awake methods, as they may not be assigned yet.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Reference to the player object.")]
    GameObject player;
    [SerializeField]
    [Tooltip("Reference to the Translator component.")]
    Translator translator;
    [SerializeField]
    [Tooltip("Reference to the InputManager component.")]
    InputManager inputManager;
    [SerializeField]
    [Tooltip("Reference to the AudioController component.")]
    AudioController audioController;
    [SerializeField]
    [Tooltip("Referenceto the SceneTracker component.")]
    SceneTracker sceneTracker;

    static GameObject instancePlayer = null;
    static Translator instanceTranslator = null;
    static InputManager instanceInputManager = null;
    static AudioController instanceAudioController = null;
    static SceneTracker instanceSceneTracker = null;

    private void Awake()
    {
        instancePlayer = player;
        instanceTranslator = translator;
        instanceInputManager = inputManager;
        instanceAudioController = audioController;
        instanceSceneTracker = sceneTracker;
    }

    public static GameObject GetPlayer()
    {
        return instancePlayer;
    }
    public static Translator GetTranslator()
    {
        return instanceTranslator;
    }
    public static InputManager GetInputManager()
    {
        return instanceInputManager;
    }
    public static AudioController GetAudioController()
    {
        return instanceAudioController;
    }
    public static SceneTracker GetSceneTracker()
    {
        return instanceSceneTracker;
    }

    public static void SetPlayer(GameObject player)
    {
        instancePlayer = player;
    }
}