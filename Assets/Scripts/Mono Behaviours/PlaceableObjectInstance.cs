using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlaceableObjectInstance : MonoBehaviour
{
    private PlaceableObject _object;
    public void SetObject(PlaceableObject obj) => _object = obj;

    private PlaceableSurface[] _surfaces;

    private bool _isMoving;
    public void SetIsMoving(bool isMoving) => _isMoving = isMoving;

    [SerializeField] private float _holdTime;
    private float _currentHoldTime;

    private void Start()
    {
        _surfaces = FindObjectsByType<PlaceableSurface>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
            .Where(item => item.SurfaceType == _object.ItemType)
            .ToArray();
    }

    private void Update()
    {
        var touches = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches;
        
        if (_isMoving)
        {
            if (touches.Count == 0)
            {
                _isMoving = false;
                return;
            }

            Move(touches[0]);
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(touches[0].screenPosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, Physics.AllLayers, QueryTriggerInteraction.Collide) && hit.collider.gameObject == gameObject)
            {
                _isMoving = true;
            }
        }
    }

    public void Move(UnityEngine.InputSystem.EnhancedTouch.Touch touch)
    {
        Vector2 screenPosition = touch.screenPosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        transform.position = worldPosition;
    }

    public void DisplayObjectPopup()
    {
        PopupMethods.Display(_object.name, _object.Description, _object.Icon, _object.Buttons);
    }
}
