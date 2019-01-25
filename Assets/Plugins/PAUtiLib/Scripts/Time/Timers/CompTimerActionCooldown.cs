// Author(s): Paul Calande
// A composite timer.
// Used for a period of time spent performing an action followed by a cooldown time
// before the action can be performed again.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompTimerActionCooldown
{
    // The timer that runs while the action is being performed.
    Timer timerAction;
    // The timer that runs while the cooldown is occurring.
    Timer timerCooldown;
    // The callback that should happen when the action timer finishes.
    Timer.FinishedHandler actionFinishedCallback;

    // Callback for the action timer.
    private void TimerActionCallback(float secondsOverflow)
    {
        // Action finished. Start the cooldown.
        timerCooldown.Run(secondsOverflow);
        actionFinishedCallback(secondsOverflow);
    }

    public CompTimerActionCooldown(float secondsAction, float secondsCooldown,
        Timer.FinishedHandler actionFinishedCallback = null,
        Timer.FinishedHandler cooldownFinishedCallback = null)
    {
        this.actionFinishedCallback = actionFinishedCallback;
        timerAction = new Timer(secondsAction, TimerActionCallback, false);
        timerCooldown = new Timer(secondsCooldown, cooldownFinishedCallback, false);
    }

    // Returns true if the composite timer is running.
    // Returns false if the timer is on standby and ready to start.
    public bool IsRunning()
    {
        return timerAction.IsRunning() || timerCooldown.IsRunning();
    }

    // Returns true if the action is being performed.
    public bool IsActionRunning()
    {
        return timerAction.IsRunning();
    }

    // Returns true if the cooldown is in progress.
    public bool IsCooldownRunning()
    {
        return timerCooldown.IsRunning();
    }

    // Tries to start the action timer, effectively performing the action.
    // Returns true if it starts successfully. Returns false otherwise.
    // It will return false if the composite timer is already running.
    public bool Run()
    {
        // If either timer is running, don't start the action timer.
        if (IsRunning())
        {
            return false;
        }
        timerAction.Run();
        return true;
    }

    // Increments the time for both timers.
    public void Tick(float deltaTime)
    {
        timerAction.Tick(deltaTime);
        timerCooldown.Tick(deltaTime);
    }
}