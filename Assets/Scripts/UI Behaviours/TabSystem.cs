using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TabSystem : UIBehaviour
{
    [SerializeField] private Tab[] _tabs;
    [SerializeField] private int _selectedIndex;
    [SerializeField] private UnityEvent<int> _onTabSwitch;

    protected override void Reset()
    {
        _tabs = GetComponentsInChildren<Tab>(true);
    }

    protected override void Awake()
    {
        TabTo(_selectedIndex);
    }

    protected override void OnValidate()
    {
        Awake();
    }

    public void TabTo(int index)
    {
        _selectedIndex = ValidateIndex(index);

        for (int i = 0; i < _tabs.Length; ++i)
        {
            _tabs[i].gameObject.SetActive(i == index);

            if (i == index)
                _tabs[i].InvokeOnOpen();
        }

        _onTabSwitch.Invoke(_selectedIndex);
    }

    public void TabTo(Tab tab) => TabTo(GetIndexOf(tab));
    public void TabBy(int delta) => TabTo(_selectedIndex + delta);

    public int GetIndexOf(Tab tab) => Array.IndexOf(_tabs, tab);
    public Tab GetTabAt(int index) => _tabs[index];

    private int ValidateIndex(int index) => Math.Clamp(index, 0, _tabs.Length - 1);
}
