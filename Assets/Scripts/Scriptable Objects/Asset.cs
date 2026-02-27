using UnityEngine;

[CreateAssetMenu(fileName = "Asset", menuName = "Scriptable Objects/Asset")]
public abstract class Asset : ScriptableObject
{
    [SerializeField] private string _description;
    public string Description => _description;

    [SerializeField] private int _price;
    public int Price => _price;

    [SerializeField] private GameObject _object;
    public GameObject Object => _object;

    public abstract void Interact();
}
