using System.Linq;
using UnityEngine;

/// <summary>
/// System for creating and manipulating popups.
/// </summary>
public static class PopupMethods
{
    /// <summary>
    /// Spawns a poup with the given title, description, image, and buttons.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="description"></param>
    /// <param name="image"></param>
    /// <param name="buttons"></param>
    public static void Display(string title, string description, Sprite image, ButtonInfo[] buttons)
    {
        PopupSystem.Instance.Display(title, description, image, buttons);
    }

    /// <summary>
    /// Spawns a poup with the given title, description, image, and buttons.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="description"></param>
    /// <param name="image"></param>
    /// <param name="buttons"></param>
    public static void Display(string title, string description, Sprite image, UnityButtonInfo[] buttons)
    {
        PopupSystem.Instance.Display(title, description, image, buttons.Cast<ButtonInfo>().ToArray());
    }

    /// <summary>
    /// Spawns a popup from a Scriptable Object.
    /// </summary>
    /// <param name="popup"></param>
    public static void Display(Popup popup)
    {
        Display(popup.Title, popup.Description, popup.Image, popup.Buttons);
    }
}
