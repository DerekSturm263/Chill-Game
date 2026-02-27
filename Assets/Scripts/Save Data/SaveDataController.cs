using UnityEngine;

public class SaveDataController : SingletonBehaviour<SaveDataController>
{
    [SerializeField][Tooltip("The full file name to store the save data under. This needs to end with a file extension")] private string _fileName;
    public string FileName => _fileName;

    [SerializeField][Tooltip("The default save data to load. When the player starts a new game or resets their data, this is used")] private SaveDataAsset _default;
    [HideInInspector] public SaveData current;

    public void Save()
    {
        SerializationHelper.Save(current, _fileName);
    }
    
    public void Load()
    {
        current = SerializationHelper.Load(_default.Value, _fileName);
    }
    
    public void Delete()
    {
        SerializationHelper.Delete(_fileName);
    }

    public override void StartUp() => Save();
    public override void ShutDown() => Load();
}
