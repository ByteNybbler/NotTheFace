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

    protected void Start()
    {
        distributor.AddSubscriber(this);
    }

    public abstract void ReceiveInput(InputReader inputReader);
}