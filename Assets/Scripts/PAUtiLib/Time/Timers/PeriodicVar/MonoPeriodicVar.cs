// Author(s): Paul Calande
// MonoBehaviour wrapper for a PeriodicVar.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoPeriodicVar<T> : MonoBehaviour
{
    protected PeriodicVar<T> pvar = new PeriodicVar<T>(default(T), 1.0f);

    public void SetVar(T var)
    {
        pvar.SetVar(var);
    }

    public void SetSeconds(float seconds)
    {
        pvar.SetSeconds(seconds);
    }

    public T GetVar()
    {
        return pvar.GetVar();
    }

    public float GetSeconds()
    {
        return pvar.GetSeconds();
    }

    public PeriodicVar<T> Get()
    {
        return pvar;
    }
}