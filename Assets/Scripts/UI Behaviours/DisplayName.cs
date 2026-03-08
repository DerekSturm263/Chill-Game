using UnityEngine;
using UnityEngine.Events;

public class DisplayName : MonoBehaviour
{
    [SerializeField] private UnityEvent<string> _onDisplay;

    private void OnEnable()
    {
        _onDisplay.Invoke(SaveMethods.Current.username);
    }

    private void Update()
    {
        _onDisplay.Invoke(SaveMethods.Current.username);
    }
}
