using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoldableButton : Button, IPointerClickHandler, IPointerUpHandler
{
    public float holdTime = 3f;
    public UnityEvent onHold;

    private float _originalTime;

    private void LongPress()
    {
        if (!IsActive() || !IsInteractable())
            return;

        UISystemProfilerApi.AddMarker("Button.onHold", this);
        onHold.Invoke();
    }

    public new virtual void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        _originalTime = Time.time;
    }

    public new virtual void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        if (Time.time - _originalTime >= holdTime)
            LongPress();
    }
}
