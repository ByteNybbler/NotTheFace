// Author(s): Paul Calande
// Parent class for classes that subscribe to InputDistributors.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputDistributed : MonoBehaviour, IPlayable
{
    [SerializeField]
    [Tooltip("Reference to the InputDistributor to subscribe to.")]
    protected InputDistributor distributor;

    protected virtual void Start()
    {
        SubscribeToDistributor();
    }

    public void SubscribeToDistributor()
    {
        if (distributor != null)
        {
            distributor.AddSubscriber(this);
        }
    }

    public void UnsubscribeFromDistributor()
    {
        if (distributor != null)
        {
            distributor.RemoveSubscriber(this);
        }
    }

    public abstract void ReceiveInput(InputReader inputReader);
}