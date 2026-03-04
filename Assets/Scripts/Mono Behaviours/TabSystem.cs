using UnityEngine;

public class TabSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] _tabs;

    public void SwitchTab(GameObject tab)
    {
        for (int i = 0; i < _tabs.Length; ++i)
        {
            _tabs[i].SetActive(_tabs[i] == tab);
        }
    }
}
