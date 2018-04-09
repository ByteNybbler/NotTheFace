// Author(s): Paul Calande
// A trigger that only fires its events when interacting with objects with the given tags.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTags2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Which tags to accept (or deny, if inverted is true).")]
    string[] tags;
    [SerializeField]
    [Tooltip("If true, the tags will be a blacklist instead of a whitelist.")]
    bool inverted;
    [SerializeField]
    [Tooltip("Trigger enter event.")]
    TriggerEvent2D triggerEnter;
    [SerializeField]
    [Tooltip("Trigger exit event.")]
    TriggerEvent2D triggerExit;

    // Returns true if the given collision's tag satisfies the conditions.
    private bool IsTagValid(Collider2D collision)
    {
        bool result = inverted;
        foreach (string tagName in tags)
        {
            if (collision.CompareTag(tagName))
            {
                result = !result;
                break;
            }
        }
        return result;
    }

    // Returns true if the given collision can be accepted by the trigger at this time.
    private bool IsValid(Collider2D collision)
    {
        return IsTagValid(collision) && isActiveAndEnabled;
    }

    private void InvokeEvent(TriggerEvent2D e, Collider2D collision)
    {
        if (e != null)
        {
            e.OnInvoke(collision);
        }
    }

    private void TryInvokeEvent(TriggerEvent2D e, Collider2D collision)
    {
        if (IsValid(collision))
        {
            InvokeEvent(e, collision);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TryInvokeEvent(triggerEnter, collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TryInvokeEvent(triggerExit, collision);
    }
}