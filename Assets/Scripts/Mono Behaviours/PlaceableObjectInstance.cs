using System.Linq;
using UnityEngine;

public class PlaceableObjectInstance : MonoBehaviour
{
    private PlaceableObject _item;
    private PlaceableSurface[] _surfaces;

    public static PlaceableObjectInstance CreateFromItem(PlaceableObject item)
    {
        GameObject newItem = new()
        {
            name = item.name
        };

        PlaceableObjectInstance component = newItem.GetComponent<PlaceableObjectInstance>();

        component._item = item;
        component._surfaces = FindObjectsByType<PlaceableSurface>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
            .Where(item => item.SurfaceType == component._item.ItemType).ToArray();

        return component;
    }

    public void Move(Vector3 position)
    {

    }
}
