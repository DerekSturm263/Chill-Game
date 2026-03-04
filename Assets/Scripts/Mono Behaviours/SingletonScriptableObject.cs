using UnityEngine;

public abstract class SingletonScriptableObject : ScriptableObject
{
    public abstract void StartUp();
    public abstract void ShutDown();
}
