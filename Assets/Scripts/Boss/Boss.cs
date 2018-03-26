// Author(s): Paul Calande
// General script for a Not the Face boss.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    /*
    public class Data
    {
        public class Refs
        {
            [Tooltip("The room that the boss inhabits.")]
            Room room;

            public Refs(Room room)
            {
                this.room = room;
            }
        }
        Refs refs;

        public Data(Refs refs)
        {
            this.refs = refs;
        }
    }
    Data data;

    public void SetData(Data data)
    {
        this.data = data;
    }
    */

    public delegate void DiedHandler();
    public event DiedHandler Died;

    [SerializeField]
    [Tooltip("The health of the boss.")]
    Health healthBoss;

    private void Start()
    {
        healthBoss.Died += OnDied;
    }

    private void OnDied()
    {
        Destroy(gameObject);
        if (Died != null)
        {
            Died();
        }
    }
}