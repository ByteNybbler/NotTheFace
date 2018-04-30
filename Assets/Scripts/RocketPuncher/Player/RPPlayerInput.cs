// Author(s): Paul Calande
// Player input class for Rocket Puncher.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPPlayerInput : MonoBehaviour, IControllable
{
    [System.Serializable]
    public class Data
    {
        [Tooltip("Rocket movement speed.")]
        public float movementSpeed;

        public Data(float movementSpeed)
        {
            this.movementSpeed = movementSpeed;
        }
    }
    [SerializeField]
    Data data;

    [SerializeField]
    [Tooltip("Reference to the Mover component.")]
    Mover2D mover;
    [SerializeField]
    [Tooltip("Reference to the PlayerPunch component.")]
    RPPlayerPunch playerPunch;
    [SerializeField]
    TimeScale tsGameplay;
    [SerializeField]
    TimeScale tsBackground;
    [SerializeField]
    [Tooltip("Reference to the pause menu.")]
    GameObject pauseMenu;
    [SerializeField]
    [Tooltip("HierarchyAnimationToggler for the player.")]
    HierarchyAnimationToggler hat;

    public void SetData(Data val)
    {
        data = val;
    }

    private void Start()
    {
        ServiceLocator.GetInputManager().AddSubscriber(this);
        pauseMenu.SetActive(false);
    }

    private void OnDestroy()
    {
        ServiceLocator.GetInputManager().RemoveSubscriber(this);
    }

    public void TogglePause()
    {
        tsGameplay.TogglePause();
        tsBackground.TogglePause();
        hat.Toggle();
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void ReceiveInput(InputReader inputReader)
    {
        float axisH = inputReader.GetAxisHorizontalRaw();
        float axisV = inputReader.GetAxisVerticalRaw();
        Vector2 change = new Vector2(axisH, axisV)
            * data.movementSpeed * tsGameplay.DeltaTime();
        mover.OffsetPosition(change);

        if (!tsGameplay.IsPaused())
        {
            if (inputReader.GetKeyDown(KeyCode.Space))
            {
                playerPunch.Punch();
            }
        }

        if (inputReader.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
}