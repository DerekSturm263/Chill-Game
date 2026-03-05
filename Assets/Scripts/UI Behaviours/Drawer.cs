using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class Drawer : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public enum DragAxis
    {
        X,
        Y
    }

    [SerializeField] private float _lerpSpeed;
    [SerializeField] private DragAxis _axis;
    [SerializeField] private float _velocityToSwipe;
    [SerializeField] private float _closedPadding;
    [SerializeField] private float _openPadding;
    [SerializeField] private bool _isOpen;
    
    public void SetIsOpen(bool isOpen) => _isOpen = isOpen;

    private bool _isLerping;
    private float _blendValue;

    private RectTransform _rectTransform;
    private RectTransform _parentRect;

    private Vector2 ClosePos
    {
        get
        {
            if (_axis == DragAxis.X)
                return new(-_parentRect.sizeDelta.x + _closedPadding, 0);
            else
                return new(0, -_parentRect.sizeDelta.y + _closedPadding);
        }
    }
    private Vector2 OpenPos
    {
        get
        {
            if (_axis == DragAxis.X)
                return new(-_openPadding, 0);
            else
                return new(0, -_openPadding);
        }
    }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _parentRect = transform.parent.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (_isLerping)
        {
            float target = _isOpen ? 1f : 0f;
            _blendValue = Mathf.Lerp(_blendValue, target, Time.deltaTime * _lerpSpeed);

            _rectTransform.anchoredPosition = Vector2.Lerp(ClosePos, OpenPos, _blendValue);
        }
    }

    private void OnValidate()
    {
        Awake();
        _rectTransform.anchoredPosition = _isOpen ? OpenPos : ClosePos;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        _isLerping = false;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector2 newPos = transform.position;

        if (_axis == DragAxis.X)
            newPos.x += eventData.delta.x;
        else
            newPos.y += eventData.delta.y;

        transform.position = newPos;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        _isLerping = true;

        if (_axis == DragAxis.X)
        {
            if (_isOpen && eventData.delta.x < -_velocityToSwipe)
                SetIsOpen(false);
            else if (!_isOpen && eventData.delta.x > _velocityToSwipe)
                SetIsOpen(true);

            _blendValue = Mathf.InverseLerp(ClosePos.x, OpenPos.x, _rectTransform.anchoredPosition.x);
        }
        else
        {
            if (_isOpen && eventData.delta.y < -_velocityToSwipe)
                SetIsOpen(false);
            else if (!_isOpen && eventData.delta.y > _velocityToSwipe)
                SetIsOpen(true);

            _blendValue = Mathf.InverseLerp(ClosePos.y, OpenPos.y, _rectTransform.anchoredPosition.y);
        }
    }
}
