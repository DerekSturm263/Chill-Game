using UnityEngine;

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

    public override void OnTap()
    {
        throw new System.NotImplementedException();
    }

    public override void OnHold()
    {
        throw new System.NotImplementedException();
    }
}
