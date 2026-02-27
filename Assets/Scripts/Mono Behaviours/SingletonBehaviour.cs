using UnityEngine;

public abstract class SingletonBehaviourBase : MonoBehaviour
{
    public abstract void StartUp();
    public abstract void ShutDown();
}

public abstract class SingletonBehaviour<TSelf> : SingletonBehaviourBase where TSelf : SingletonBehaviour<TSelf>, new()
{
    private static TSelf _instance;
    public static TSelf Instance => _instance;

    private void Awake()
    {
        if (_instance)
        {
            Debug.LogWarning($"An instance of {nameof(TSelf)} ({_instance.name}) already exists. The new instance ({name}) will be ignored");
            return;
        }

        _instance = this as TSelf;
    }
}
