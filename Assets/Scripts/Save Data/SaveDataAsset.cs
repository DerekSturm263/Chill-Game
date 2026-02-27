using UnityEngine;

[CreateAssetMenu(fileName = "Save Data Asset", menuName = "Scriptable Objects/Save Data Asset")]
public class SaveDataAsset : ScriptableObject
{
    [SerializeField] private SaveData _value;
    public SaveData Value => _value;
}
