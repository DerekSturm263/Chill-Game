using System.Linq;
using UnityEngine;

public class Initializer : SingletonBehaviour<Initializer>
{
    [SerializeField] private SingletonBehaviourBase[] _behaviours;

    public override void StartUp()
    {
        foreach (var behaviour in _behaviours)
        {
            behaviour.StartUp();
        }
    }

    public override void ShutDown()
    {
        foreach (var behaviour in _behaviours.Reverse())
        {
            behaviour.ShutDown();
        }
    }

    private void Start() => StartUp();
    private void OnDestroy() => ShutDown();
}
