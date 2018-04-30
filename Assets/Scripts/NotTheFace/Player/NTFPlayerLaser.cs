// Author(s): Paul Calande
// Laser gun for Not the Face.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NTFPlayerLaser : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The laser's damage component.")]
    Damage damage;
    [SerializeField]
    [Tooltip("The laser to activate and deactivate.")]
    Laser2D laser;

    private void Start()
    {
        laser.gameObject.SetActive(false);
    }

    public void Fire(bool right)
    {
        laser.gameObject.SetActive(true);
        //Debug.Log("PlayerLaser laser global scale: " + laser.transform.lossyScale);
    }

    public int GetDamage()
    {
        return damage.Get();
    }
}