// Author(s): Paul Calande
// Input script for pausing the game.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPause : MonoBehaviour, IControllable
{
    [SerializeField]
    [Tooltip("The time scale to pause.")]
    TimeScale timeScaleGameplay;
    [SerializeField]
    [Tooltip("The pause menu GameObject to enable and disable.")]
    GameObject pauseMenu;

    private void Start()
    {
        ServiceLocator.GetInputManager().AddSubscriber(this);
	}

    private void OnDestroy()
    {
        ServiceLocator.GetInputManager().RemoveSubscriber(this);
    }

    public void ReceiveInput(InputReader inputReader)
    {
        if (inputReader.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        timeScaleGameplay.TogglePause();
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }
}