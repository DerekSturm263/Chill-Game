using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Placeable Object", menuName = "Scriptable Objects/Placeable Object")]
public class PlaceableObject : Asset
{
    public enum Type
    {
        Floor,
        Wall,
        Ceiling
    }

    [SerializeField] private Type _type;
    public Type ItemType => _type;

    [SerializeField] private Vector3Int _dimensions;
    public Vector3Int Dimensions => _dimensions;

    [SerializeField] private UnityButtonInfo[] _buttons;
    public UnityButtonInfo[] Buttons => _buttons;

    public override void OnTapObject(GameObject obj)
    {
        Debug.Log("Tap Object");

        obj.GetComponent<PlaceableObjectInstance>().DisplayObjectPopup();
    }

    public override void OnHoldObject(GameObject obj)
    {
        Debug.Log("Hold Object");

        obj.GetComponent<PlaceableObjectInstance>().SetIsMoving(true);
    }
}
