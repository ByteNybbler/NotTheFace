// Author(s): Paul Calande
// Component for an orb attack from a Not the Face boss.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOrb : MonoBehaviour
{
    public delegate void IdleFinishedHandler();
    public event IdleFinishedHandler IdleFinished;

    [System.Serializable]
    public class Data
    {
        public int damage;
        public float secondsToIdle;
        public float secondsToIdleVariance;
        public float secondsToMoveToCenter;
        public float secondsToMoveToBottom;
        public float horizontalSpeed;

        public Data(int damage, float secondsToIdle, float secondsToIdleVariance,
            float secondsToMoveToCenter, float secondsToMoveToBottom,
            float horizontalSpeed)
        {
            this.damage = damage;
            this.secondsToIdle = secondsToIdle;
            this.secondsToIdleVariance = secondsToIdleVariance;
            this.secondsToMoveToCenter = secondsToMoveToCenter;
            this.secondsToMoveToBottom = secondsToMoveToBottom;
            this.horizontalSpeed = horizontalSpeed;
        }
    }
    Data data;

    [SerializeField]
    TimeScale timeScale;
    [SerializeField]
    [Tooltip("Lerp machine for moving to the center of the boss and down to the floor.")]
    LerpToPositions2D lerper;
    [SerializeField]
    [Tooltip("Oscillator for moving the orb up and down.")]
    OscillatePosition2D oscillator;
    [SerializeField]
    [Tooltip("The orb's velocity component once it finishes lerping.")]
    Velocity2D velocity;
    [SerializeField]
    [Tooltip("The orb's damage component.")]
    Damage damage;
    [SerializeField]
    [Tooltip("The orb's animator.")]
    Animator animator;
    [SerializeField]
    [Tooltip("The Transform of the object that the orb targets.")]
    Transform target;
    [SerializeField]
    [Tooltip("The sound that plays when the orb is thrown.")]
    AudioClip soundThrow;

    Timer timerIdle;

    public void SetData(Data val)
    {
        data = val;
    }

    public void SetCenterAndBottom(Vector2 posCenter, Vector2 posBottom)
    {
        LerpToPositions2D.Node[] nodes = new LerpToPositions2D.Node[2];
        nodes[0] = new LerpToPositions2D.Node(posCenter,
            LerpToPositions2D.Node.LerpType.Seconds, data.secondsToMoveToCenter);
        nodes[1] = new LerpToPositions2D.Node(posBottom,
            LerpToPositions2D.Node.LerpType.Seconds, data.secondsToMoveToBottom);
        lerper.SetNodes(nodes);
    }

    public void SetAnimatorController(RuntimeAnimatorController rac)
    {
        animator.runtimeAnimatorController = rac;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void Start()
    {
        oscillator.enabled = false;
        lerper.enabled = true;
        lerper.Completed += LerpFinish;

        damage.Add(data.damage);

        timerIdle = new Timer(UtilRandom.RangeWithCenter(data.secondsToIdle,
            data.secondsToIdleVariance), TimerIdleFinish, false);
        timerIdle.Run();
    }

    private void TimerIdleFinish(float secondsOverflow)
    {
        lerper.BeginWithFirstNode();
        OnIdleFinished();
        ServiceLocator.GetAudioController().PlaySFX(soundThrow);
    }

    private void LerpFinish()
    {
        lerper.enabled = false;
        oscillator.enabled = true;

        // Set the orb's velocity.
        float targetX = target ? target.position.x : 0.0f;
        float signDirection = Mathf.Sign(targetX - transform.position.x);
        Vector2 vel = new Vector2(signDirection * data.horizontalSpeed, 0.0f);
        velocity.SetVelocity(vel);
    }

    private void FixedUpdate()
    {
        timerIdle.Tick(timeScale.DeltaTime());
    }

    private void OnIdleFinished()
    {
        if (IdleFinished != null)
        {
            IdleFinished();
        }
    }
}