using System.Linq;
using UnityEngine;

public class PlaceableObjectInstance : MonoBehaviour
{
    private PlaceableObject _item;
    private PlaceableSurface[] _surfaces;

    private bool _isMoving;
    public void SetIsMoving(bool isMoving) => _isMoving = isMoving;

    public static PlaceableObjectInstance CreateFromItem(PlaceableObject item)
    {
        GameObject newItem = new()
        {
            name = item.name
        };

        PlaceableObjectInstance component = newItem.GetComponent<PlaceableObjectInstance>();

        component._item = item;
        component._surfaces = FindObjectsByType<PlaceableSurface>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
            .Where(item => item.SurfaceType == component._item.ItemType)
            .ToArray();
        component._isMoving = true;

        return component;
    }

    private void Update()
    {
        if (_isMoving)
        {
            var touches = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches;
            if (touches.Count == 0)
            {
                _isMoving = false;
                return;
            }

            Move(touches[0]);
        }
    }

    private void Move(UnityEngine.InputSystem.EnhancedTouch.Touch touch)
    {
        Vector2 screenPosition = touch.screenPosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        transform.position = worldPosition;
    }

    public void DisplayObjectPopup()
    {

    }
}
