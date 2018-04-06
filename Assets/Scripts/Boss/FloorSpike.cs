// Author(s): Paul Calande
// Floor spike script for Not the Face.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpike : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        [Tooltip("How much damage the object does.")]
        public int damage;
        [Tooltip("The warning GameObject to use before the spike appears.")]
        public GameObject prefabWarning;
        [Tooltip("How many seconds the warning should last.")]
        public float secondsOfWarning;
        [Tooltip("How many seconds it takes for the object to reach its end point.")]
        public float secondsOfRising;
        [Tooltip("How many seconds the object should idle at its end point.")]
        public float secondsOfIdling;
        [Tooltip("How many seconds it takes for the object to lower back to its start.")]
        public float secondsOfLowering;
        [Tooltip("The actual height in units the object should rise up.")]
        public float heightToRise;
        [Tooltip("The random variance in how high the spikes will rise.")]
        public float heightToRiseVariance;

        public Data(int damage,
            GameObject prefabWarning,
            float secondsOfWarning, float secondsOfRising,
            float secondsOfIdling, float secondsOfLowering,
            float heightToRise, float heightToRiseVariance)
        {
            this.damage = damage;
            this.prefabWarning = prefabWarning;
            this.secondsOfWarning = secondsOfWarning;
            this.secondsOfRising = secondsOfRising;
            this.secondsOfIdling = secondsOfIdling;
            this.secondsOfLowering = secondsOfLowering;
            this.heightToRise = heightToRise;
            this.heightToRiseVariance = heightToRiseVariance;
        }
    }
    [SerializeField]
    Data data;

    public enum State
    {
        Warning,
        Rise,
        Idle,
        Lower
    }

    [SerializeField]
    TimeScale timeScale;
    [SerializeField]
    Mover2D mover;
    [SerializeField]
    Damage damage;
    [SerializeField]
    [Tooltip("How high above the object's origin the warning should appear.")]
    float warningHeight;

    // How high the spike will rise up.
    float heightToRise;

    State state = State.Warning;
    Vector2 velocity;
    Vector2 target;
    Timer timerWarning;
    Timer timerIdle;

    public void SetData(Data val)
    {
        data = val;
    }

    private void Start()
    {
        timerWarning = new Timer(data.secondsOfWarning, WarningFinish, false, false);
        timerIdle = new Timer(data.secondsOfIdling, IdleFinish, false, false);
        timerWarning.Start();
        GameObject warning = Instantiate(data.prefabWarning,
            transform.position + Vector3.up * warningHeight,
            Quaternion.identity);
        warning.GetComponent<TimeToDestroy>().Set(data.secondsOfWarning);
        damage.Add(data.damage);
        heightToRise = UtilRandom.RangeWithCenter(data.heightToRise, data.heightToRiseVariance);
    }

    private void WarningFinish(float secondsOverflow)
    {
        state = State.Rise;
        Vector3 currentPosition = mover.GetPosition();
        target = currentPosition + Vector3.up * heightToRise;
        velocity = UtilPredict.ConstantVelocity(currentPosition,
            target, data.secondsOfRising);
    }

    private void IdleFinish(float secondsOverflow)
    {
        state = State.Lower;
        Vector3 currentPosition = mover.GetPosition();
        target = currentPosition - Vector3.up * data.heightToRise;
        velocity = UtilPredict.ConstantVelocity(currentPosition,
            target, data.secondsOfLowering);
    }

    private void FixedUpdate()
    {
        float dt = timeScale.DeltaTime();

        if (velocity != Vector2.zero)
        {
            mover.TeleportPosition(
                UtilApproach.Position2(mover.GetPosition(), target, velocity * dt));
            if (mover.GetPosition() == target)
            {
                // The object has reached its destination.
                // Do different things depending on the current state.
                if (state == State.Rise)
                {
                    state = State.Idle;
                    timerIdle.Start();
                }
                else if (state == State.Lower)
                {
                    Destroy(gameObject);
                }
            }
        }

        timerWarning.Tick(dt);
        timerIdle.Tick(dt);
    }
}