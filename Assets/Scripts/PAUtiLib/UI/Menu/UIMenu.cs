// Author(s): Paul Calande
// Script for a menu containing multiple items.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    [SerializeField]
    [Tooltip("All of the menu items contained in this menu.")]
    UIMenuItem[] menuItems;

    // The index of the currently-selected menu item in the array.
    int selectedIndex = -1;

    private void Start()
    {
        foreach (UIMenuItem item in menuItems)
        {
            item.SetMenu(this);
        }
        SelectItemIndex(0);
    }

    // Returns the number of items in the menu.
    private int GetTotalMenuItems()
    {
        return menuItems.Length;
    }

    // Returns the index of the final menu item in the array.
    private int GetLastMenuItemIndex()
    {
        return GetTotalMenuItems() - 1;
    }

    // Returns the currently-selected menu item.
    private UIMenuItem GetCurrentItem()
    {
        return menuItems[selectedIndex];
    }

    // Selects the current menu item.
    private void SelectCurrentItem()
    {
        GetCurrentItem().Select();
    }

    // Deselects the currently-selected menu item.
    private void DeselectCurrentItem()
    {
        GetCurrentItem().Deselect();
    }

    // Selects an item based on its index.
    private void SelectItemIndex(int index)
    {
        // Don't do anything if the target index is the same as the currently-selected index.
        if (index != selectedIndex)
        {
            DeselectCurrentItem();
            selectedIndex = index;
            SelectCurrentItem();
        }
    }

    // Returns the item index that matches the given item.
    private int GetItemIndex(UIMenuItem item)
    {
        for (int i = 0; i < GetTotalMenuItems(); ++i)
        {
            if (item == menuItems[i])
            {
                return i;
            }
        }
        Debug.LogError("UIMenu.GetItemIndex: Could not find " + item.name);
        return -1;
    }

    // Selects a specific menu item.
    public void SelectSpecificItem(UIMenuItem item)
    {
        SelectItemIndex(GetItemIndex(item));
    }

    // Move the menu selection by the given number of menu items.
    public void MoveSelection(int amount)
    {
        // The target index is the index of the soon-to-be-selected menu item.
        int targetIndex = (selectedIndex + amount) % GetLastMenuItemIndex();
        SelectItemIndex(targetIndex);
    }

    // Presses right on the currently-selected menu item.
    public void PressRightOnSelection()
    {
        GetCurrentItem().PressRight();
    }

    // Presses left on the currently-selected menu item.
    public void PressLeftOnSelection()
    {
        GetCurrentItem().PressLeft();
    }
}