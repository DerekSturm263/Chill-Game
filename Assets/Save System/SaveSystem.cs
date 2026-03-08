using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Save System", menuName = "Save System/Save System")]
public class SaveSystem : ScriptableObject
{
    internal const string SaveDataFileExtension = ".savedata";

    private static readonly string[] AssetPath = { "Settings", "Save System", "Save System.asset" };

    [SerializeField][Tooltip("The key for encrypting/decrypting your save data.")] private byte[] _key =
    {
        0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
    };

#if UNITY_EDITOR

    [SerializeField][Tooltip("The script determining which variables are tracked by the save system. Add any serialiable values to this script to save and load them.")] private UnityEditor.MonoScript _variableScript = null;

#endif

    [SerializeField][Tooltip("The path of your save file. This will automatically be put inside of Application.persistentDataPath.")] private string _saveDataFolder = "Save Data";
    [SerializeField][Tooltip("The default values for the save system. When starting a new save, SaveSystem.Current will be reset to these.")] private SaveDataAsset _default;
    [SerializeField][Tooltip("Whether or not to automatically load the save data when the game is started. This will read from \"main.savedata\" in your Save Data folder")] private bool _autoLoadOnStart = true;
    [SerializeField][Tooltip("Whether or not to automatically save the save data when the game is quit. This will write to \"main.savedata\" in your Save Data folder")] private bool _autoSaveOnQuit = true;

    [SerializeField] internal SaveData _current;

#if UNITY_EDITOR

    public static SaveSystem Instance
    {
        get
        {
            if (!UnityEditor.AssetDatabase.IsValidFolder($"Assets/{AssetPath[0]}"))
                UnityEditor.AssetDatabase.CreateFolder("Assets", AssetPath[0]);

            if (!UnityEditor.AssetDatabase.IsValidFolder($"Assets/{AssetPath[0]}/{AssetPath[1]}"))
                UnityEditor.AssetDatabase.CreateFolder($"Assets/{AssetPath[0]}", AssetPath[1]);

            string fullPath = Path.Combine("Assets", AssetPath[0], AssetPath[1], AssetPath[2]);
            SaveSystem settings = UnityEditor.AssetDatabase.LoadAssetAtPath<SaveSystem>(fullPath);

            if (!settings)
            {
                settings = CreateInstance<SaveSystem>();

                UnityEditor.AssetDatabase.CreateAsset(settings, fullPath);
                UnityEditor.AssetDatabase.SaveAssets();
            }

            return settings;
        }
    }

    internal static UnityEditor.SerializedObject GetSerializedObject() => new(Instance);

    internal void OpenVariablesInEditor()
    {
        string fullPath = UnityEditor.AssetDatabase.GetAssetPath(_variableScript);

        if (File.Exists(fullPath))
        {
            UnityEditor.EditorUtility.OpenWithDefaultApp(fullPath);
        }
        else
        {
            Debug.LogError($"File not found: {fullPath}");
        }
    }

    internal void OpenSaveFolderInExplorer()
    {
        UnityEditor.EditorUtility.RevealInFinder(SerializationHelper.GetFullPath(_saveDataFolder, ""));
    }

#endif

    [RuntimeInitializeOnLoadMethod]
    private static void OnStartup() => Instance.Startup("main");

    private void Startup(string fileName)
    {
        _current = default;

        if (_autoLoadOnStart)
            Load(fileName);

        Application.quitting += () =>
        {
            if (_autoSaveOnQuit)
                Save(fileName);
            
            _current = default;
        };
    }

    internal void Save(string fileName)
    {
        SerializationHelper.Save(_current, _saveDataFolder, fileName + SaveDataFileExtension, _key);

        foreach (var item in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            item.BroadcastMessage("OnSave", SendMessageOptions.DontRequireReceiver);
        }
    }

    internal void Load(string fileName)
    {
        _current = SerializationHelper.Load(_default ? _default.Value : default, _saveDataFolder, fileName + SaveDataFileExtension, _key);

        foreach (var item in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            item.BroadcastMessage("OnLoad", SendMessageOptions.DontRequireReceiver);
        }
    }

    internal void Erase(string fileName)
    {
        SerializationHelper.Delete(_saveDataFolder, fileName + SaveDataFileExtension);

        foreach (var item in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            item.BroadcastMessage("OnErase", SendMessageOptions.DontRequireReceiver);
        }
    }

    internal void EraseAndReset(string fileName)
    {
        Erase(fileName);
        Load(fileName);
    }

    internal void EraseAllSaveFiles()
    {
        foreach (string fileName in GetAllSaveFileNames())
        {
            Erase(fileName.Replace(SaveDataFileExtension, ""));
        }
    }

    internal IEnumerable<SaveData> GetAllSaveFiles()
    {
        return SerializationHelper.GetAllFilesInDirectory<SaveData>(_saveDataFolder, _key);
    }

    internal IEnumerable<string> GetAllSaveFileNames()
    {
        return SerializationHelper.GetAllFileNamesInDirectory(_saveDataFolder);
    }

    internal void GenerateKey()
    {
        _key = new byte[16];

        for (int i = 0; i < _key.Length; ++i)
        {
            _key[i] = (byte)Random.Range(0, byte.MaxValue + 1);
        }
    }

    private void Reset()
    {
        GenerateKey();

#if UNITY_EDITOR

        // TODO: Grab SaveData.cs as the default variable script

#endif
    }
}
