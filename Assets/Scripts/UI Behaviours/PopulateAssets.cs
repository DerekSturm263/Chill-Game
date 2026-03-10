using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopulateAssets : MonoBehaviour
{
    [System.Flags]
    public enum Filter
    {
        Locked = 1 << 0,
        Unlocked = 1 << 1
    }

    [SerializeField] private Filter _filter;
    [SerializeField] private string _resourcePath;
    [SerializeField] private bool _loadOnEnable;
    [SerializeField] private HoldableButton _button;
    [SerializeField] private float _holdTime;
    [SerializeField] private UnityEvent<Asset> _onTap;
    [SerializeField] private UnityEvent<Asset> _onHold;

    private void OnEnable()
    {
        if (_loadOnEnable)
            Load();
    }

    private void OnDisable()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }

    [ContextMenu("Load")]
    public void Load()
    {
        Asset[] items = Resources.LoadAll<Asset>(_resourcePath)
            .Where(item => (_filter.HasFlag(Filter.Unlocked) && item.IsUnlocked) || (_filter.HasFlag(Filter.Locked) && !item.IsUnlocked))
            .ToArray();

        foreach (var item in items)
        {
            HoldableButton button = Instantiate(_button, transform);
            button.name = $"{item.name} Button";
            button.holdTime = _holdTime;

            Image icon = button.gameObject.FindChildByTag("Icon")?.GetComponent<Image>();
            if (icon)
                icon.sprite = item.Icon;

            button.gameObject.FindChildByTag("Label")?.GetComponent<TMPro.TMP_Text>()?.SetText(item.name);

            button.transform.SetParent(transform);
            button.onClick.AddListener(() => _onTap.Invoke(item));
            button.onHold.AddListener(() => _onHold.Invoke(item));
        }
    }

    public void SetupButton(Button button, Asset asset)
    {
        button.GetComponentInChildren<Image>().sprite = asset.Icon;
    }
}
