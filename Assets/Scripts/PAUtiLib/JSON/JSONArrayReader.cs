// Author(s): Paul Calande
// Wrapper class for a JSON array, with some additional convenient methods.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class JSONArrayReader
{
    // The array that is being read.
    JSONArray myArray;
    // The number of elements in the array.
    int count;
    // The current index in the array being stepped through.
    int currentArrayIndex = 0;

    public JSONArrayReader(JSONArray myArray)
    {
        SetArray(myArray);
    }

    private void SetArray(JSONArray myArray)
    {
        this.myArray = myArray;
        count = myArray.Count;
    }

    private JSONNodeReader GetNode(int index)
    {
        return new JSONNodeReader(myArray[index]);
    }

    // Returns false if there are no nodes left to read.
    // The node to be read is assigned to nodeReader.
    public bool GetNextNode(out JSONNodeReader nodeReader)
    {
        if (currentArrayIndex < count)
        {
            nodeReader = GetNode(currentArrayIndex);
            ++currentArrayIndex;
            return true;
        }
        else
        {
            nodeReader = null;
            return false;
        }
    }
}