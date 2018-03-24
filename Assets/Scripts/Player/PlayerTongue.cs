// Author(s): Paul Calande
// Tongue script for Not the Face.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTongue : MonoBehaviour
{
    public class Data
    {
        public float secondsTongueAction;
        public float secondsTongueCooldown;
        public Data(float secondsTongueAction, float secondsTongueCooldown)
        {
            this.secondsTongueAction = secondsTongueAction;
            this.secondsTongueCooldown = secondsTongueCooldown;
        }
    }
    Data data;
    public void SetData(Data val)
    {
        data = val;
    }

    [SerializeField]
    TimeScale timeScale;
    [SerializeField]
    [Tooltip("Reference to the tongue GameObject.")]
    GameObject tongue;
    [SerializeField]
    [Tooltip("Reference to the player's component for axis storage.")]
    InputStoreNonzeroAxes storeNonzeroAxes;

    CompTimerActionCooldown timerTongue;

    private void Start()
    {
        timerTongue = new CompTimerActionCooldown(data.secondsTongueAction,
            data.secondsTongueCooldown,
            TongueFinished);
        tongue.SetActive(false);
    }

    private void TongueFinished(float secondsOverflow)
    {
        tongue.SetActive(false);
    }

    private void FixedUpdate()
    {
        timerTongue.Tick(timeScale.DeltaTime());
    }

    public void Fire()
    {
        if (timerTongue.TryStart())
        {
            Vector3 scale = tongue.transform.localScale;
            scale.x = storeNonzeroAxes.GetSignHorizontal();
            tongue.transform.localScale = scale;
            tongue.SetActive(true);
        }
    }
}