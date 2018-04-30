// Author(s): Paul Calande
// BoolAccessor support for activating and deactivating GameObjects.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolAccessorGameObjectActive : SingleAccessorConnection
    <bool, BoolAccessor, GameObject>
{
    protected override void Set(bool data)
    {
        connected.SetActive(data);
    }

    protected override bool Get()
    {
        return connected.activeSelf;
    }
}