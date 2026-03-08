using UnityEngine;

[CreateAssetMenu(fileName = "New Save Data", menuName = "Save System/Save Data")]
public class SaveDataAsset : ScriptableObject
{
    [SerializeField] private SaveData _value;
    public SaveData Value => _value;
}
