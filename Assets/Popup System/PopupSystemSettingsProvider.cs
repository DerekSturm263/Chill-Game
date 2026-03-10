#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public static class PopupSystemSettingsProvider
{
    [SettingsProvider]
    public static SettingsProvider CreateProvider() => new("Project/Popup System", SettingsScope.Project)
    {
        label = "Popup System",
        activateHandler = (_, rootElement) =>
        {
            SerializedObject serializedObject = PopupSystem.GetSerializedObject();
            PopupSystem popupSystem = serializedObject.targetObject as PopupSystem;

            #region Heading

            rootElement.Add(new Label("Popup System")
            {
                name = "header",
                style =
                {
                    unityFontStyleAndWeight = FontStyle.Bold,
                    fontSize = 20,
                    marginLeft = 10,
                    marginBottom = 6
                }
            });

            #endregion

            #region Objects

            ObjectField popupSystemObject = new("Popup System")
            {
                name = "popupSystem",
                tooltip = "The Popup System to use. The settings below are based on this object.",
                objectType = typeof(PopupSystem),
                value = serializedObject.targetObject,
                style =
                {
                    marginBottom = 5
                }
            };

            rootElement.Add(popupSystemObject);

            rootElement.Add(new PropertyField(serializedObject.FindProperty("_popup")));
            rootElement.Add(new PropertyField(serializedObject.FindProperty("_button")));

            #endregion

            popupSystemObject.RegisterValueChangedCallback(_ =>
            {
                serializedObject.Update();
                rootElement.MarkDirtyRepaint();
            });

            rootElement.Bind(serializedObject);
        },
        keywords = new HashSet<string>(new[] { "Popup", "Button" })
    };
}

#endif