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
    [Tooltip("Reference to the tongue parent object.")]
    GameObject tongue;
    [SerializeField]
    [Tooltip("Reference to the player's component for axis storage.")]
    InputStoreNonzeroAxes storeNonzeroAxes;
    [SerializeField]
    [Tooltip("The animator to run for the lick animation.")]
    Animator animator;
    [SerializeField]
    [Tooltip("Rotates the head to face the licking direction.")]
    RotateGraduallyToAngle2D tongueHeadRotator;
    [SerializeField]
    [Tooltip("Reference to the parent object of the non-tongue rotators.")]
    GameObject normalRotators;

    CompTimerActionCooldown timerTongue;

    static int hashLick = Animator.StringToHash("Lick");

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
        normalRotators.SetActive(true);
    }

    private void FixedUpdate()
    {
        timerTongue.Tick(timeScale.DeltaTime());
    }

    public void Fire(bool right)
    {
        if (timerTongue.TryStart())
        {
            Vector3 scale = tongue.transform.localScale;
            scale.x = UtilMath.Sign(right);
            tongue.transform.localScale = scale;
            tongue.SetActive(true);
            animator.SetTrigger(hashLick);
            tongueHeadRotator.SetAngle(UtilHeading2D.DegreesFromBoolean(right));
            normalRotators.SetActive(false);
        }
    }

    public bool IsTongueRunning()
    {
        return timerTongue.IsActionRunning();
    }
}