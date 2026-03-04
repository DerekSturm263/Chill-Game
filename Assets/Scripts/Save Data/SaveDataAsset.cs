using UnityEngine;

[CreateAssetMenu(fileName = "New Save Data", menuName = "Scriptable Objects/Save Data")]
public class SaveDataAsset : ScriptableObject
{
    [SerializeField] private SaveData _value;
    public SaveData Value => _value;
}
