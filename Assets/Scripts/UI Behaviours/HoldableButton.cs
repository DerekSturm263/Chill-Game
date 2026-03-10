using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HoldableButton : Button, IPointerDownHandler, IPointerUpHandler
{
    public float holdTime = 3f;
    public UnityEvent onHold;

    private bool _isHolding;
    private float _holdTime;

    private void Update()
    {
        if (_isHolding)
        {
            _holdTime += Time.deltaTime;

            if (_holdTime > holdTime)
            {
                LongPress();

                _holdTime = 0;
                _isHolding = false;
            }
        }
    }

    private void LongPress()
    {
        if (!IsActive() || !IsInteractable())
            return;

        UISystemProfilerApi.AddMarker("Button.onHold", this);
        onHold.Invoke();
    }

    public new virtual void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        _holdTime = 0;
        _isHolding = true;
    }

    public new virtual void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        _holdTime = 0;
        _isHolding = false;
    }
}
