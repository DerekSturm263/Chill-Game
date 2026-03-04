using UnityEngine;

[CreateAssetMenu(fileName = "Asset", menuName = "Scriptable Objects/Asset")]
public abstract class Asset : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    public Sprite Icon => _icon;

    [SerializeField] private int _price;
    public int Price => _price;

    [SerializeField][Multiline] private string _description;
    public string Description => _description;

    [SerializeField] private GameObject _object;
    public GameObject Object => _object;

    public bool IsUnlocked => SaveSystem.Current.inventory[this] >= 0;

    public abstract void OnTap();
    public abstract void OnHold();
}
