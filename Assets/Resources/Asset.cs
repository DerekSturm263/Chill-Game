using System;
using UnityEngine;
using UnityEngine.UI;

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

    public bool IsUnlocked => SaveMethods.Current.inventory[this] >= 0;
    public uint Count => (uint)Math.Clamp(SaveMethods.Current.inventory[this], uint.MinValue, uint.MaxValue);
}
