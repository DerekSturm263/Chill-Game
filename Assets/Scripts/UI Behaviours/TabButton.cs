using UnityEngine;
using UnityEngine.UI;

public class TabButton : Button
{
    [SerializeField] private TabSystem _tabSystem;
    [SerializeField] private Tab _tab;

    protected override void Reset()
    {
        _tabSystem = GetComponentInParent<TabSystem>();
        
        int index = transform.GetSiblingIndex();
        _tab = _tabSystem.GetTabAt(index);
    }

    protected override void Awake()
    {
        onClick.AddListener(() => _tabSystem.TabTo(_tab));
    }
}
