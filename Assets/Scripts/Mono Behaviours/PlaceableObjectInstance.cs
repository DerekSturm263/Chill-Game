using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlaceableObjectInstance : MonoBehaviour
{
    [SerializeField] private float _holdTime;
    [SerializeField] private LayerMask _layer;

    private PlaceableObject _object;
    public void SetObject(PlaceableObject obj) => _object = obj;

    private bool _isMoving;
    public void SetIsMoving(bool isMoving) => _isMoving = isMoving;

    private LayerMask _objectLayer;
    private float _currentHoldTime;

    private void Awake()
    {
        _objectLayer = 1 << gameObject.layer;
    }

    private void Update()
    {
        var touches = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches;
        if (touches.Count == 0)
        {
            _isMoving = false;
            return;
        }

        if (_isMoving)
        {
            Move(touches[0]);
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(touches[0].screenPosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, _objectLayer, QueryTriggerInteraction.Collide) && hit.collider.gameObject == gameObject)
                _isMoving = true;

            foreach (var surface in FindObjectsByType<PlaceableSurface>(FindObjectsInactive.Exclude, FindObjectsSortMode.None))
            {
                surface.SetIsOn(1 << surface.gameObject.layer == _layer);
            }
        }
    }

    public void Move(UnityEngine.InputSystem.EnhancedTouch.Touch touch)
    {
        Vector2 screenPosition = touch.screenPosition;
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100, _layer, QueryTriggerInteraction.Collide))
        {
            transform.position = Vector3Int.RoundToInt(hit.point);
            transform.up = hit.normal;
        }
        else
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPosition);
            transform.position = worldPos;
        }
    }

    public void DisplayObjectPopup()
    {
        PopupMethods.Display(_object.name, _object.Description, _object.Icon, _object.Buttons);
    }
}
