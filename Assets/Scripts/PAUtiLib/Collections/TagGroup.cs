// Author(s): Paul Calande
// A collection of strings that can be checked to see if it contains a given string.
// This is most useful for comparing tags, hence the class name.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TagGroup
{
    [SerializeField]
    [Tooltip("Which tags to accept (or deny, if inverted is true).")]
    string[] tags;

    // Returns true if the given string is contained within the collection
    // (or not, if inverted is true).
    public bool IsValid(string tag, bool inverted)
    {
        bool result = inverted;
        foreach (string tagName in tags)
        {
            if (tag == tagName)
            {
                result = !result;
                break;
            }
        }
        return result;
    }

    // Returns true if the given collision's tag is contained within the collection
    // (or not, if inverted is true).
    public bool IsValid(Collider2D collision, bool inverted)
    {
        return IsValid(collision.tag, inverted);
    }
}