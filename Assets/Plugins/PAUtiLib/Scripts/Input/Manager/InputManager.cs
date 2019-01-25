// Author(s): Paul Calande
// Class that collects input every frame for use in FixedUpdate.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    InputWriter inputWriter;
    InputReader inputReader;
    HashSet<IControllable> subscribers = new HashSet<IControllable>();

    private void Awake()
    {
        InputData inputData = new InputData();
        inputWriter = new InputWriter(inputData);
        inputReader = new InputReader(inputData);
    }

    private void Update()
    {
        inputWriter.PopulateKeys();
    }

    private void FixedUpdate()
    {
        inputWriter.PopulateAxes();
        SendInputToSubscribers();
        inputWriter.Clear();
    }

    public void AddSubscriber(IControllable subscriber)
    {
        subscribers.Add(subscriber);
    }

    public void RemoveSubscriber(IControllable subscriber)
    {
        subscribers.Remove(subscriber);
    }

    public bool HasSubscriber(IControllable subscriber)
    {
        return subscribers.Contains(subscriber);
    }

    public void ToggleSubscriber(IControllable subscriber)
    {
        if (HasSubscriber(subscriber))
        {
            RemoveSubscriber(subscriber);
        }
        else
        {
            AddSubscriber(subscriber);
        }
    }

    private void SendInputToSubscribers()
    {
        HashSet<IControllable> iterateSubscribers = new HashSet<IControllable>(subscribers);
        foreach (IControllable sub in iterateSubscribers)
        {
            sub.ReceiveInput(inputReader);
        }
    }
}