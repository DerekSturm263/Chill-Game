using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SaveDataController))]
public class SaveDataControllerEditor : Editor
{
    private SerializedProperty _fileName;
    private SerializedProperty _default;
    private SerializedProperty _current;

    void OnEnable()
    {
        _fileName = serializedObject.FindProperty("_fileName");
        _default = serializedObject.FindProperty("_default");
        _current = serializedObject.FindProperty("_current");
    }
    
    public override void OnInspectorGUI()
    {
        SaveDataController controller = target as SaveDataController;

        serializedObject.Update();

        EditorGUILayout.PropertyField(_fileName);
        EditorGUILayout.PropertyField(_default);

        if (GUILayout.Button("Save"))
        {
            controller.Save();
        }

        if (GUILayout.Button("Load"))
        {
            controller.Load();
        }

        if (GUILayout.Button("Delete"))
        {
            controller.Delete();
        }

        if (GUILayout.Button("Open"))
        {
            EditorUtility.RevealInFinder(SerializationHelper.CreateDirectory(controller.FileName));
        }

        if (Application.isPlaying)
        {
            EditorGUILayout.PropertyField(_current);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
