// Author(s): Paul Calande
// MonoBehaviour wrapper for a TimedVar.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoTimedVar<T> : MonoBehaviour
{
    protected TimedVar<T> pvar = new TimedVar<T>(default(T), 1.0f);

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

    public TimedVar<T> Get()
    {
        return pvar;
    }
}