using System.Linq;
using UnityEngine;

public class Initializer : SingletonBehaviour<Initializer>
{
    [SerializeField] private SingletonScriptableObject[] _singletons;

    public override void StartUp()
    {
        foreach (var behaviour in _singletons)
        {
            behaviour.StartUp();
        }
    }

    public override void ShutDown()
    {
        foreach (var behaviour in _singletons.Reverse())
        {
            behaviour.ShutDown();
        }
    }

    private void Start() => StartUp();
    private void OnDestroy() => ShutDown();
}
