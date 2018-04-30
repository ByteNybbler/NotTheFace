// Author(s): Paul Calande
// Component that instantiates a GameObject and notifies certain other instantiation-related
// components about this instantiation.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    // Invoked when checking if conditions are valid for instantiating the GameObject.
    public delegate void ValidationStartedHandler();
    public event ValidationStartedHandler ValidationStarted;
    // Invoked when the GameObject is successfully instantiated.
    public delegate void InstantiatedHandler(GameObject obj, float secondsOverflow);
    public event InstantiatedHandler Instantiated;

    [SerializeField]
    [Tooltip("The GameObject to instantiate.")]
    GameObject toInstantiate;
    [SerializeField]
    [Tooltip("The transform to parent the instantiated object to. If null, no parent " +
        "will be used.")]
    Transform parent = null;

    bool conditionsValid;

    // Attempts to instantiate the GameObject.
    public void Instantiate(float secondsOverflow = 0.0f)
    {
        conditionsValid = true;
        OnValidationStarted();
        if (conditionsValid)
        {
            GameObject obj = Instantiate(toInstantiate, parent);
            obj.transform.position = transform.position;
            OnInstantiated(obj, secondsOverflow);
        }
    }

    // To be called by validator components when the conditions are not valid.
    public void Invalidate()
    {
        conditionsValid = false;
    }

    private void OnValidationStarted()
    {
        if (ValidationStarted != null)
        {
            ValidationStarted();
        }
    }

    private void OnInstantiated(GameObject obj, float secondsOverflow)
    {
        if (Instantiated != null)
        {
            Instantiated(obj, secondsOverflow);
        }
    }
}