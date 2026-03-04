using UnityEngine;

/// <summary>
/// System for saving and loading custom save data. Utilize the static functions for save data management.
/// </summary>
[CreateAssetMenu(fileName = "New Save System", menuName = "Save System")]
public class SaveSystem : ScriptableObject
{
    private const string AssetPath = "Settings/Save System/Save System.asset";

    [SerializeField][Tooltip("The key for encrypting/decrypting your save data.")] private byte[] _key =
    {
        0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
    };

    [SerializeField][Tooltip("The path to the save system's variables. Any serializable value in this script will be saved and loaded.")] private string _variablesPath = "Settings/Save Data/SaveData.cs";
    [SerializeField][Tooltip("The name of your save data file. This will be stored in your application's persistent data path.")] private string _fileName = ".savedata";
    [SerializeField][Tooltip("The default values for the save system. When starting a new save, SaveSystem.Current will get reset to these.")] private SaveDataAsset _default;
    [SerializeField][Tooltip("Whether or not to automatically load the save data when the game is started.")] private bool _autoLoadOnStart = true;
    [SerializeField][Tooltip("Whether or not to automatically save the save data when the game is quit.")] private bool _autoSaveOnQuit = true;

    /// <summary>
    /// The current values for the save system. Get and set these values during runtime.
    /// </summary>
    public static SaveData Current;

#if UNITY_EDITOR

    public static SaveSystem GetOrCreateSettings()
    {
        string fullPath = System.IO.Path.Combine(Application.dataPath, AssetPath);
        SaveSystem settings = UnityEditor.AssetDatabase.LoadAssetAtPath<SaveSystem>(fullPath);

        if (!settings)
        {
            settings = CreateInstance<SaveSystem>();

            UnityEditor.AssetDatabase.CreateAsset(settings, AssetPath);
            UnityEditor.AssetDatabase.SaveAssets();
        }

        return settings;
    }

    public static UnityEditor.SerializedObject GetSerializedObject() => new(GetOrCreateSettings());

    public void OpenVariablesInEditor()
    {
        string fullPath = System.IO.Path.Combine(Application.dataPath, _variablesPath);

        if (System.IO.File.Exists(fullPath))
        {
            UnityEditor.EditorUtility.OpenWithDefaultApp(fullPath);
        }
        else
        {
            Debug.LogError($"File not found: {fullPath}");
        }
    }

    public void OpenSaveDataInExplorer()
    {
        UnityEditor.EditorUtility.RevealInFinder(SerializationHelper.CreateDirectory(_fileName));
    }

#endif

    [RuntimeInitializeOnLoadMethod]
    private static void OnStartup()
    {
        GetOrCreateSettings().Startup();
    }

    private void Startup()
    {
        if (_autoLoadOnStart)
            Load();

        if (_autoSaveOnQuit)
            Application.quitting += Save;
    }

    /// <summary>
    /// Save the values of <see cref="Current"/>.
    /// </summary>
    public void Save()
    {
        SerializationHelper.Save(Current, _fileName, _key);
    }

    /// <summary>
    /// Load values to <see cref="Current"/>.
    /// </summary>
    public void Load()
    {
        Current = SerializationHelper.Load(_default ? _default.Value : default, _fileName, _key);
    }
    
    /// <summary>
    /// Erase the currently saved values from your files.
    /// </summary>
    public void Erase()
    {
        SerializationHelper.Delete(_fileName);
    }

    /// <summary>
    /// Erase the currently saved values from your files and reset <see cref="Current"/>.
    /// </summary>
    public void EraseAndReset()
    {
        SerializationHelper.Delete(_fileName);
        Load();
    }

    /// <summary>
    /// Generate a new encryption/decryption key.
    /// </summary>
    public void GenerateKey()
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
    }
}
