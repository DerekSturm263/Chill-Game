using System.Linq;

/// <summary>
/// System for saving and loading custom save data. Utilize the static functions for save data management.
/// </summary>
public static class SaveMethods
{
    private const string DefaultSaveFileName = "main";

    /// <summary>
    /// Your current save data values. Read and write from this variable to have your changes tracked by the save system.
    /// </summary>
    public static ref SaveData Current => ref SaveSystem.Instance._current;

    /// <summary>
    /// Saves <see cref="Current"/> to a save file. If the file doesn't exist, one is created. Will also broadcast "OnSave" to all GameObjects in the scene.
    /// </summary>
    /// <param name="fileName">The file name to save. For example, "player1".</param>
    public static void Save(string fileName = DefaultSaveFileName) => SaveSystem.Instance.Save(fileName);

    /// <summary>
    /// Loads the save file into <see cref="Current"/>. If the file doesn't exist, <see cref="Current"/> is set to the default values specified in the Save System asset. Will also broadcast "OnLoad" to all GameObjects in the scene.
    /// </summary>
    /// <param name="fileName">The file name to load. For example, "player1".</param>
    public static void Load(string fileName = DefaultSaveFileName) => SaveSystem.Instance.Load(fileName);

    /// <summary>
    /// Erase the save file. This will delete the save file from the device. Will also broadcast "OnErase" to all GameObjects in the scene.
    /// </summary>
    /// <param name="fileName">The file name to erase. For example, "player1".</param>
    public static void Erase(string fileName = DefaultSaveFileName) => SaveSystem.Instance.Erase(fileName);

    /// <summary>
    /// Erase the save file and reset <see cref="Current"/> to the default values specified in the Save System asset.
    /// </summary>
    /// <param name="fileName">The file name to erase. For example, "player1".</param>
    public static void EraseAndReset(string fileName = DefaultSaveFileName) => SaveSystem.Instance.EraseAndReset(fileName);

    /// <summary>
    /// Erase all save files. This will delete all save files from the device.
    /// </summary>
    public static void EraseAllSaveFiles() => SaveSystem.Instance.EraseAllSaveFiles();

    /// <summary>
    /// Get all save files from the device.
    /// </summary>
    /// <returns></returns>
    public static SaveData[] GetAllSaveFiles() => SaveSystem.Instance.GetAllSaveFiles().ToArray();

    /// <summary>
    /// Get the file names of all save files from the device. This is much quicker than loading each file if you don't need to access all the save data values.
    /// </summary>
    /// <returns></returns>
    public static string[] GetAllSaveFileNames() => SaveSystem.Instance.GetAllSaveFileNames().ToArray();
}
