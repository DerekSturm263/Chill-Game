using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SaveSystem))]
public class SaveSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Open Save System Settings Window"))
        {
            UnityEditor.SettingsService.OpenProjectSettings("Project/Save System");
        }

        EditorGUILayout.HelpBox("This Save System is assigned as the active Save System", MessageType.Info);
    }
}
