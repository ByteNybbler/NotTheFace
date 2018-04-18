// Author(s): Paul Calande
// Distributes input from an InputManager to a variety of subscribers.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDistributor : MonoBehaviour, IPlayable
{
    [SerializeField]
    [Tooltip("Whether to subscribe to the Service Locator's Input Manager immediately.")]
    bool immediatelySubscribeToManager;

    // Reference to the InputManager to use.
    InputManager im;
    HashSet<IPlayable> subscribers = new HashSet<IPlayable>();

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

    public void AddSubscriber(IPlayable subscriber)
    {
        subscribers.Add(subscriber);
    }

    public void RemoveSubscriber(IPlayable subscriber)
    {
        subscribers.Remove(subscriber);
    }

    public void ReceiveInput(InputReader inputReader)
    {
        HashSet<IPlayable> toIterate = new HashSet<IPlayable>(subscribers);
        foreach (IPlayable subscriber in toIterate)
        {
            subscriber.ReceiveInput(inputReader);
        }
    }
}