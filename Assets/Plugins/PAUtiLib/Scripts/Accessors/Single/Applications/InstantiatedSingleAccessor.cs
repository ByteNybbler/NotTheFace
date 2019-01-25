// Author(s): Paul Calande
// Transfers an accessor value to an instantiated object's accessor.

using UnityEngine;

public class InstantiatedSingleAccessor<TValue, TAccessor> : MonoBehaviour
    where TAccessor : SingleAccessor<TValue>
{
    [SerializeField]
    [Tooltip("The instantiator to subscribe to.")]
    Instantiator instantiator;
    [SerializeField]
    [Tooltip("The accessor to copy the value from.")]
    TAccessor accessor;
    
    private void Awake()
    {
        instantiator.Instantiated += Instantiated;
    }

    private void Instantiated(GameObject obj, float secondsOverflow)
    {
        obj.GetComponent<TAccessor>().Set(accessor.Get());
    }
}