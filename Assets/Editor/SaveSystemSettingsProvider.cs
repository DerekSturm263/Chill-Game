using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public static class SaveSystemSettingsProvider
{
    [SettingsProvider]
    public static SettingsProvider CreateProvider() => new("Project/Save System", SettingsScope.Project)
    {
        label = "Save System",
        activateHandler = (_, rootElement) =>
        {
            SerializedObject serializedObject = SaveSystem.GetSerializedObject();
            SaveSystem saveSystem = serializedObject.targetObject as SaveSystem;

            rootElement.Add(new Label()
            {
                name = "header",
                text = "Save System",
                style =
                {
                    unityFontStyleAndWeight = FontStyle.Bold,
                    fontSize = 20,
                    marginLeft = 10,
                    marginBottom = 6
                }
            });

            rootElement.Add(new ObjectField()
            {
                name = "saveSystem",
                label = "Save System",
                tooltip = "The Save System to use. The settings below are based on this object.",
                objectType = typeof(SaveSystem),
                value = serializedObject.targetObject
            });
            rootElement.Add(new PropertyField(serializedObject.FindProperty("_variablesPath")));

            SerializedProperty defaultProperty = serializedObject.FindProperty("_default");

            rootElement.Add(new PropertyField(defaultProperty));

            //SerializedObject defaultObjectObject = new(defaultProperty.objectReferenceValue);
            //rootElement.Add(new PropertyField(defaultObjectObject.FindProperty("_value")));



            // Key.

            rootElement.Add(new Label()
            {
                name = "keyLabel",
                text = "Key"
            });

            VisualElement keyRow = new()
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                    flexWrap = Wrap.Wrap
                }
            };

            SerializedProperty keyProperty = serializedObject.FindProperty("_key");
            for (int i = 0; i < keyProperty.arraySize; ++i)
            {
                PropertyField byteField = new(keyProperty.GetArrayElementAtIndex(i));
                keyRow.Add(byteField);
            }

            rootElement.Add(keyRow);



            // Save/Load Settings.

            rootElement.Add(new Label()
            {
                name = "saveLabel",
                text = "Save/Load Settings",
                style =
                {
                    unityFontStyleAndWeight = FontStyle.Bold,
                    marginTop = 10
                }
            });

            rootElement.Add(new PropertyField(serializedObject.FindProperty("_autoLoadOnStart")));
            rootElement.Add(new PropertyField(serializedObject.FindProperty("_autoSaveOnQuit"))
            {
                style =
                {
                    marginBottom = 10
                }
            });



            // Actions.

            rootElement.Add(new Label()
            {
                name = "actionsLabel",
                text = "Actions",
                style =
                {
                    unityFontStyleAndWeight = FontStyle.Bold
                }
            });

            VisualElement buttonRow = new()
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                    flexWrap = Wrap.Wrap
                }
            };

            buttonRow.Add(new Button(saveSystem.OpenVariablesInEditor)
            {
                name = "editVariables",
                text = "Edit Variables",
                tooltip = "Edit the save system's variables.",
                style =
                {
                    height = 25
                }
            });
            buttonRow.Add(new Button(saveSystem.GenerateKey)
            {
                name = "generateKey",
                text = "Generate New Key",
                tooltip = "Generate a new key for data encryption. This will delete all old save data.",
                style =
                {
                    height = 25
                }
            });
            buttonRow.Add(new Button(saveSystem.Save)
            {
                name = "save",
                text = "Save",
                tooltip = "Save the current values to your save file.",
                style =
                {
                    height = 25
                }
            });
            buttonRow.Add(new Button(saveSystem.Load)
            {
                name = "load",
                text = "Load",
                tooltip = "Load the values from your save file.",
                style =
                {
                    height = 25
                }
            });
            buttonRow.Add(new Button(saveSystem.Erase)
            {
                name = "erase",
                text = "Erase",
                tooltip = "Permanently erase your save file.",
                style =
                {
                    height = 25
                }
            });
            buttonRow.Add(new Button(saveSystem.EraseAndReset)
            {
                name = "eraseAndReset",
                text = "Erase & Reset",
                tooltip = "Permanently erase your save file and reset the current values.",
                style =
                {
                    height = 25
                }
            });
            buttonRow.Add(new Button(saveSystem.OpenSaveDataInExplorer)
            {
                name = "openInExplorer",
                text = "Open In Explorer",
                tooltip = "Open the File Explorer to your encrypted save file.",
                style =
                {
                    height = 25
                }
            });

            rootElement.Add(buttonRow);

            rootElement.Bind(serializedObject);
        },
        keywords = new HashSet<string>(new[] { "Default", "Key", "Auto Load on Start", "Auto Save on Quit", "Current"})
    };

}
