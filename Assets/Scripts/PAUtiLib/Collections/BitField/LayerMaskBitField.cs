// Author(s): Paul Calande
// Wrapper class for a layer mask that utilizes the BitField class.
// Makes it easy to modify the mask on a layer-by-layer basis.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerMaskBitField : IBitFieldMaskable<LayerMask>,
    IBitFieldIndexable<int>,
    IBitFieldIndexable<string>
{
    BitField field = new BitField();

    public LayerMaskBitField(LayerMask mask)
    {
        SetMask(mask);
    }

    public LayerMaskBitField(params string[] layerNames)
    {
        foreach (string layerName in layerNames)
        {
            SetIndex(layerName);
        }
    }

    public LayerMaskBitField(params int[] layerNumbers)
    {
        foreach (int layerNumber in layerNumbers)
        {
            SetIndex(layerNumber);
        }
    }

    public int GetInt()
    {
        return field.GetInt();
    }

    private int GetLayerIndex(string layerName)
    {
        return LayerMask.NameToLayer(layerName);
    }

    public void SetMask(LayerMask mask)
    {
        field.SetMask(mask.value);
    }

    public void ClearMask(LayerMask mask)
    {
        field.ClearMask(mask.value);
    }

    public void ToggleMask(LayerMask mask)
    {
        field.ToggleMask(mask.value);
    }

    public void SetIndex(int layerNumber)
    {
        field.SetIndex(layerNumber);
    }

    public void ClearIndex(int layerNumber)
    {
        field.ClearIndex(layerNumber);
    }

    public void ToggleIndex(int layerNumber)
    {
        field.ToggleIndex(layerNumber);
    }

    public bool IsIndexSet(int layerNumber)
    {
        return field.IsIndexSet(layerNumber);
    }

    public void SetIndex(string layerName)
    {
        SetIndex(GetLayerIndex(layerName));
    }

    public void ClearIndex(string layerName)
    {
        ClearIndex(GetLayerIndex(layerName));
    }

    public void ToggleIndex(string layerName)
    {
        ToggleIndex(GetLayerIndex(layerName));
    }

    public bool IsIndexSet(string layerName)
    {
        return IsIndexSet(GetLayerIndex(layerName));
    }
}