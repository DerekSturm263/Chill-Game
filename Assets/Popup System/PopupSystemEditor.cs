#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PopupSystem))]
public class PopupSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Open Popup System Settings Window"))
        {
            SettingsService.OpenProjectSettings("Project/Popup System");
        }

        EditorGUILayout.HelpBox("This Popup System is assigned as the active Popup System", MessageType.Info);
    }
}

#endif