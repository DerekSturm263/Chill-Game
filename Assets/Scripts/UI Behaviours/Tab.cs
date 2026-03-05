using UnityEngine;
using UnityEngine.Events;

public class Tab : MonoBehaviour
{
    [SerializeField] private UnityEvent _onOpen;
    public void InvokeOnOpen() => _onOpen.Invoke();
}
