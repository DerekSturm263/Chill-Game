using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopulateInventory : MonoBehaviour
{
    [System.Flags]
    public enum Filter
    {
        Locked = 1 << 0,
        Unlocked = 1 << 1
    }

    [SerializeField] private Filter _filter;
    [SerializeField] private string _resourcePath;
    [SerializeField] private Button _button;
    [SerializeField] private UnityEvent<Asset> _onTap;
    [SerializeField] private UnityEvent<Asset> _onHold;

    public void Load()
    {
        Asset[] items = Resources.LoadAll<Asset>(_resourcePath)
            .Where(item => (_filter.HasFlag(Filter.Unlocked) && item.IsUnlocked) || (_filter.HasFlag(Filter.Locked) && !item.IsUnlocked)).ToArray();

        foreach (var item in items)
        {
            Button button = Instantiate(_button, transform);
            
            button.transform.SetParent(transform);
            button.onClick.AddListener(() => _onTap.Invoke(item));
        }
    }

    public void SetupButton(Button button, Asset asset)
    {
        button.GetComponentInChildren<Image>().sprite = asset.Icon;
    }
}
