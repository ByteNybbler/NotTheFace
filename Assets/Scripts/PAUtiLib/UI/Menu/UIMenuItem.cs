// Author(s): Paul Calande
// Script for a single item in a menu.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuItem : MonoBehaviour
{
    public delegate void SelectedHandler();
    public event SelectedHandler Selected;
    public delegate void DeselectedHandler();
    public event DeselectedHandler Deselected;
    public delegate void FiredHandler();
    public event FiredHandler Fired;
    public delegate void PressedRightHandler();
    public event PressedRightHandler PressedRight;
    public delegate void PressedLeftHandler();
    public event PressedLeftHandler PressedLeft;

    // The menu that owns this menu item.
    UIMenu menu;

    private void OnSelected()
    {
        if (Selected != null)
        {
            Selected();
        }
    }
    private void OnDeselected()
    {
        if (Deselected != null)
        {
            Deselected();
        }
    }
    private void OnFired()
    {
        if (Fired != null)
        {
            Fired();
        }
    }
    private void OnPressedRight()
    {
        if (PressedRight != null)
        {
            PressedRight();
        }
    }
    private void OnPressedLeft()
    {
        if (PressedLeft != null)
        {
            PressedLeft();
        }
    }

    // Called when the menu item is selected, i.e. "hovered over".
    public void Select()
    {
        OnSelected();
    }

    // Called when the menu item is deselected.
    public void Deselect()
    {
        OnDeselected();
    }

    // Called when the menu item is chosen, i.e. the player presses enter or whatever.
    public void Fire()
    {
        OnFired();
    }

    // Called when the player presses right on the menu item.
    public void PressRight()
    {
        OnPressedRight();
    }

    // Called when the player presses left on the menu item.
    public void PressLeft()
    {
        OnPressedLeft();
    }

    // Sets which menu owns this menu item.
    public void SetMenu(UIMenu menu)
    {
        this.menu = menu;
    }

    // Makes the owning UIMenu select this menu item.
    public void SelectInMenu()
    {
        menu.SelectSpecificItem(this);
    }
}