// Author(s): Paul Calande
// Distributes input from an InputManager to a variety of subscribers.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDistributor : MonoBehaviour, IControllable
{
    [SerializeField]
    [Tooltip("Whether to subscribe to the Service Locator's Input Manager immediately.")]
    bool immediatelySubscribeToManager;

    // Reference to the InputManager to use.
    InputManager im;
    HashSet<IControllable> subscribers = new HashSet<IControllable>();

    private void Start()
    {
        im = ServiceLocator.GetInputManager();
        if (immediatelySubscribeToManager)
        {
            SubscribeToInputManager();
        }
    }

    private void OnDestroy()
    {
        if (im != null)
        {
            UnsubscribeFromInputManager();
        }
    }

    public void SetImmediatelySubscribeToManager(bool immediatelySubscribeToManager)
    {
        this.immediatelySubscribeToManager = immediatelySubscribeToManager;
    }

    public void SubscribeToInputManager()
    {
        im.AddSubscriber(this);
    }

    public void UnsubscribeFromInputManager()
    {
        im.RemoveSubscriber(this);
    }

    public void AddSubscriber(IControllable subscriber)
    {
        subscribers.Add(subscriber);
    }

    public void RemoveSubscriber(IControllable subscriber)
    {
        subscribers.Remove(subscriber);
    }

    public void ReceiveInput(InputReader inputReader)
    {
        HashSet<IControllable> toIterate = new HashSet<IControllable>(subscribers);
        foreach (IControllable subscriber in toIterate)
        {
            subscriber.ReceiveInput(inputReader);
        }
    }
}