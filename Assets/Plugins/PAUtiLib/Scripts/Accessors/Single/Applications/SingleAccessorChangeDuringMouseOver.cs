// Author(s): Paul Calande
// Changes an accessor's value based on whether the mouse is inside of the object.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SingleAccessorChangeDuringMouseOver<TValue, TAccessor> : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler
    where TAccessor : SingleAccessor<TValue>
{
    [SerializeField]
    [Tooltip("The component to use to modify the value.")]
    TAccessor accessor;
    [SerializeField]
    [Tooltip("The new value to assign.")]
    TValue value;

    // The runner that actually changes the value.
    RunnerChangeValue<TValue> runner;

    private void Awake()
    {
        runner = new RunnerChangeValue<TValue>(value, accessor.Set, accessor.Get);
    }

    private void Run()
    {
        runner.Run();
    }

    private void Stop()
    {
        runner.Stop();
    }

    /*
    private void OnMouseEnter()
    {
        Run();
    }

    private void OnMouseExit()
    {
        Stop();
    }
    */

    public void OnPointerEnter(PointerEventData eventData)
    {
        Run();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Stop();
    }
}