// Author(s): Paul Calande
// A collection of strings that can be checked to see if it contains a given string.
// This is most useful for comparing tags, hence the class name.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SOTagGroup", order = 10000)]
public class SOTagGroup : ScriptableObject
{
    [SerializeField]
    [Tooltip("Which tags to accept (or deny, if inverted is true).")]
    TagGroup tagGroup;
    [SerializeField]
    [Tooltip("If true, the tags will be a blacklist instead of a whitelist.")]
    bool inverted;

    // Returns true if the given string is contained within the collection
    // (or not, if inverted is true).
    public bool IsValid(string tag)
    {
        return tagGroup.IsValid(tag, inverted);
    }

    // Returns true if the given collision's tag is contained within the collection
    // (or not, if inverted is true).
    public bool IsValid(Collider2D collision)
    {
        return tagGroup.IsValid(collision.tag, inverted);
    }
}