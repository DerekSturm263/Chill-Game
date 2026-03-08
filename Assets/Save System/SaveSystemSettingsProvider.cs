#if UNITY_EDITOR

using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public static class SaveSystemSettingsProvider
{
    public const string TestFileName = "test";

    [SettingsProvider]
    public static SettingsProvider CreateProvider() => new("Project/Save System", SettingsScope.Project)
    {
        label = "Save System",
        activateHandler = (_, rootElement) =>
        {
            SerializedObject serializedObject = SaveSystem.GetSerializedObject();
            SaveSystem saveSystem = serializedObject.targetObject as SaveSystem;

            #region Heading

            rootElement.Add(new Label("Save System")
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

            ObjectField saveSystemObject = new("Save System")
            {
                name = "saveSystem",
                tooltip = "The Save System to use. The settings below are based on this object.",
                objectType = typeof(SaveSystem),
                value = serializedObject.targetObject,
                style =
                {
                    marginBottom = 5
                }
            };

            rootElement.Add(saveSystemObject);

            #endregion

            #region Tabs

            TabView tabView = new();

            UnityEngine.UIElements.Tab fileIOTab = new("File I/O")
            {
                style =
                {
                    paddingBottom = 7,
                    paddingLeft = 7,
                    paddingRight = 7,
                    paddingTop = 7
                }
            };

            UnityEngine.UIElements.Tab valuesTab = new("Values")
            {
                style =
                {
                    paddingBottom = 7,
                    paddingLeft = 7,
                    paddingRight = 7,
                    paddingTop = 7
                }
            };

            ScrollView nonScrollableValuesTab = new();

            ScrollView scrollableValuesTab = new()
            {
                style =
                {
                    flexGrow = 1
                }
            };

            valuesTab.Add(nonScrollableValuesTab);
            valuesTab.Add(scrollableValuesTab);

            UnityEngine.UIElements.Tab testingTab = new("Testing")
            {
                style =
                {
                    paddingBottom = 7,
                    paddingLeft = 7,
                    paddingRight = 7,
                    paddingTop = 7
                }
            };

            ScrollView nonScrollableTestingTab = new();

            ScrollView scrollableTestingTab = new()
            {
                style =
                {
                    flexGrow = 1
                }
            };

            testingTab.Add(nonScrollableTestingTab);
            testingTab.Add(scrollableTestingTab);

            tabView.Add(fileIOTab);
            tabView.Add(valuesTab);
            tabView.Add(testingTab);

            rootElement.Add(tabView);

            #endregion

            #region Save Folder

            VisualElement saveFolder = new()
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                    marginBottom = 10
                }
            };

            saveFolder.Add(new PropertyField(serializedObject.FindProperty("_saveDataFolder"))
            {
                style =
                {
                    flexGrow = 1
                }
            });

            saveFolder.Add(new Button(saveSystem.OpenSaveFolderInExplorer)
            {
                name = "openFolder",
                text = "Open Folder",
                tooltip = "Open the folder for your save data.",
                style =
                {
                    width = 100
                }
            });

            fileIOTab.Add(saveFolder);

            #endregion

            #region Key

            VisualElement key = new()
            {
                style =
                {
                    flexDirection = FlexDirection.Row
                }
            };

            key.Add(new Label("Key")
            {
                name = "keyLabel",
                style =
                {
                    unityFontStyleAndWeight = FontStyle.Bold,
                    marginLeft = 3,
                    flexGrow = 1
                }
            });

            key.Add(new Button(() =>
            {
                saveSystem.GenerateKey();
                saveSystem.EraseAllSaveFiles();
            })
            {
                name = "generateNew",
                text = "Generate New",
                tooltip = "Generate a new key for data encryption. This will delete all old save data.",
                style =
                {
                    width = 100
                }
            });

            fileIOTab.Add(key);

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
                keyRow.Add(new PropertyField(keyProperty.GetArrayElementAtIndex(i))
                {
                    label = "",
                    enabledSelf = false,
                    style =
                    {
                        width = 40
                    }
                });
            }

            fileIOTab.Add(keyRow);

            #endregion

            #region Save/Load Settings

            fileIOTab.Add(new Label("Save/Load Settings")
            {
                name = "saveLabel",
                style =
                {
                    unityFontStyleAndWeight = FontStyle.Bold,
                    marginTop = 10,
                    marginLeft = 3
                }
            });

            fileIOTab.Add(new HelpBox("The following options are not compatible with games that use multiple save files. Auto Load and Auto Save use the \"main.savedata\" file and are only recommended for games where multiple save files aren't present.", HelpBoxMessageType.Info)
            {
                style =
                {
                    marginBottom = 10
                }
            });

            fileIOTab.Add(new PropertyField(serializedObject.FindProperty("_autoLoadOnStart")));
            fileIOTab.Add(new PropertyField(serializedObject.FindProperty("_autoSaveOnQuit"))
            {
                style =
                {
                    marginBottom = 10
                }
            });

            #endregion

            #region Variables

            VisualElement variables = new()
            {
                style =
                {
                    flexDirection = FlexDirection.Row
                }
            };

            variables.Add(new PropertyField(serializedObject.FindProperty("_variableScript"))
            {
                style =
                {
                    flexGrow = 1
                },
                enabledSelf = false
            });

            variables.Add(new Button(saveSystem.OpenVariablesInEditor)
            {
                name = "editVariables",
                text = "Edit Variables",
                tooltip = "Edit the save system's variables.",
                style =
                {
                    width = 100
                }
            });

            nonScrollableValuesTab.Add(variables);

            #endregion

            #region Default Values

            VisualElement defaults = new()
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                    marginBottom = 10
                }
            };

            SerializedProperty defaultProperty = serializedObject.FindProperty("_default");
            PropertyField defaultPropertyField = new(defaultProperty)
            {
                style =
                {
                    flexGrow = 1
                }
            };

            defaults.Add(defaultPropertyField);

            defaults.Add(new Button(() =>
            {
                SaveDataAsset newDefault = ScriptableObject.CreateInstance<SaveDataAsset>();

                string path = AssetDatabase.GetAssetPath(serializedObject.targetObject);
                string thisFolder = Path.GetDirectoryName(path);
                AssetDatabase.CreateAsset(newDefault, Path.Combine(thisFolder, "New Default Save Data.asset"));
                AssetDatabase.SaveAssets();

                defaultProperty.objectReferenceValue = newDefault;
                serializedObject.ApplyModifiedProperties();
            })
            {
                name = "createNew",
                text = "Create New",
                tooltip = "Create and assign a new default save data.",
                style =
                {
                    width = 100
                }
            });

            nonScrollableValuesTab.Add(defaults);

            nonScrollableValuesTab.Add(new Label("Default Values")
            {
                style =
                {
                    unityFontStyleAndWeight = FontStyle.Bold,
                    marginLeft = 3
                }
            });

            if (defaultProperty.objectReferenceValue != null)
            {
                SerializedObject defaultObject = new(defaultProperty.objectReferenceValue);
                SerializedProperty defaultValue = defaultObject.FindProperty("_value");

                PropertyField defaultValueField = new(defaultValue, "")
                {
                    style =
                    {
                        marginBottom = 10
                    }
                };

                defaultPropertyField.RegisterValueChangeCallback(e =>
                {
                    serializedObject.Update();

                    if (e.changedProperty.objectReferenceValue != null)
                    {
                        defaultValueField.Bind(new(e.changedProperty.objectReferenceValue));
                        defaultValueField.MarkDirtyRepaint();
                    }
                    else
                    {
                        rootElement.MarkDirtyRepaint();
                    }
                });

                defaultValueField.Bind(defaultObject);
                scrollableValuesTab.Add(defaultValueField);
            }
            else
            {
                scrollableValuesTab.Add(new HelpBox("No default save data assigned. Please assign a Save Data Asset to use as the default values for the save system. Without one, automated default values will be chosen.", HelpBoxMessageType.Warning));

                defaultPropertyField.RegisterValueChangeCallback(e =>
                {
                    serializedObject.Update();
                    
                });
            }

            #endregion

            #region Actions

            nonScrollableTestingTab.Add(new HelpBox("The following actions are only available in Play Mode.", HelpBoxMessageType.Info)
            {
                style =
                {
                    marginBottom = 10
                },
                enabledSelf = !Application.isPlaying,

            });

            VisualElement buttonRow = new()
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                    flexWrap = Wrap.Wrap,
                    marginBottom = 10
                },
                enabledSelf = Application.isPlaying
            };

            buttonRow.Add(new Button(() => saveSystem.Save(TestFileName))
            {
                name = "save",
                text = "Save",
                tooltip = "Save the current values to a test save file.",
                style =
                {
                    height = 25
                }
            });
            buttonRow.Add(new Button(() => saveSystem.Load(TestFileName))
            {
                name = "load",
                text = "Load",
                tooltip = "Load the values from a test save file.",
                style =
                {
                    height = 25
                }
            });
            buttonRow.Add(new Button(() => saveSystem.Erase(TestFileName))
            {
                name = "erase",
                text = "Erase",
                tooltip = "Permanently erase the test save file.",
                style =
                {
                    height = 25
                }
            });
            buttonRow.Add(new Button(() => saveSystem.EraseAndReset(TestFileName))
            {
                name = "eraseAndReset",
                text = "Erase & Reset",
                tooltip = "Permanently erase the test save file and reset the current values.",
                style =
                {
                    height = 25
                }
            });

            nonScrollableTestingTab.Add(buttonRow);

            #endregion

            #region Current Values

            nonScrollableTestingTab.Add(new Label("Current Values")
            {
                style =
                {
                    unityFontStyleAndWeight = FontStyle.Bold,
                    marginLeft = 3
                },
                enabledSelf = Application.isPlaying
            });

            scrollableTestingTab.Add(new PropertyField(SaveSystem.GetSerializedObject().FindProperty("_current"), "")
            {
                enabledSelf = Application.isPlaying
            });

            #endregion

            saveSystemObject.RegisterValueChangedCallback(_ =>
            {
                serializedObject.Update();
                rootElement.MarkDirtyRepaint();
            });

            rootElement.Bind(serializedObject);
        },
        keywords = new HashSet<string>(new[] { "Default", "Key", "Auto Load on Start", "Auto Save on Quit", "Current" })
    };
}

#endif