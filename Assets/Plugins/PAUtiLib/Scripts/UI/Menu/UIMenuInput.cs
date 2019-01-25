// Author(s): Paul Calande
// Script that can be used to control UIMenus via input.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuInput : MonoBehaviour, IControllable
{
    [SerializeField]
    [Tooltip("The number of seconds between each option change when holding a button down.")]
    float secondsBetweenItemChangeOnKeyHold;
    [SerializeField]
    [Tooltip("The current menu being navigated.")]
    UIMenu menu;

    Timer timerChangeItemOnKeyHold;

    private void Start()
    {
        timerChangeItemOnKeyHold = new Timer(secondsBetweenItemChangeOnKeyHold,
            null, false);
        ServiceLocator.GetInputManager().AddSubscriber(this);
    }

    private void FixedUpdate()
    {
        timerChangeItemOnKeyHold.Tick(Time.deltaTime);
    }

    public void SetMenu(UIMenu menu)
    {
        this.menu = menu;
    }

    private void RunKeyTimer()
    {
        timerChangeItemOnKeyHold.Run();
    }

    public void ReceiveInput(InputReader inputReader)
    {
        float axisH = inputReader.GetAxisHorizontalRaw();
        float axisV = inputReader.GetAxisVerticalRaw();
        if (!timerChangeItemOnKeyHold.IsRunning())
        {
            if (axisH < 0.0f)
            {
                menu.PressLeftOnSelection();
                RunKeyTimer();
            }
            if (axisH > 0.0f)
            {
                menu.PressRightOnSelection();
                RunKeyTimer();
            }
            if (axisV < 0.0f)
            {
                menu.MoveSelection(-1);
                RunKeyTimer();
            }
            if (axisV > 0.0f)
            {
                menu.MoveSelection(1);
                RunKeyTimer();
            }
        }
        if (axisH == 0.0f && axisV == 0.0f)
        {
            timerChangeItemOnKeyHold.Stop();
            timerChangeItemOnKeyHold.Clear();
        }
    }
}