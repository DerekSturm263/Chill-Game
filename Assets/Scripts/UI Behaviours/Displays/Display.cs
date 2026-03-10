using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public abstract class Display<T> : UIBehaviour
{
    [System.Flags]
    public enum UpdateMethod
    {
        OnEnable = 1 << 0,
        Awake = 1 << 1,
        Start = 1 << 2,
        Update = 1 << 3
    }

    [SerializeField] private UpdateMethod _updateMethod;
    [SerializeField] private UnityEvent<T> _onDisplay;

    protected abstract T Value { get; }

    protected override void OnEnable()
    {
        if (_updateMethod.HasFlag(UpdateMethod.OnEnable))
            _onDisplay.Invoke(Value);
    }

    protected override void Awake()
    {
        if (_updateMethod.HasFlag(UpdateMethod.Awake))
            _onDisplay.Invoke(Value);
    }

    protected override void Start()
    {
        if (_updateMethod.HasFlag(UpdateMethod.Start))
            _onDisplay.Invoke(Value);
    }

    private void Update()
    {
        if (_updateMethod.HasFlag(UpdateMethod.Update))
            _onDisplay.Invoke(Value);
    }
}
