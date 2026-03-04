using UnityEngine;

[ExecuteAlways]
public class PlaceableSurface : MonoBehaviour
{
    [SerializeField] private PlaceableObject.Type _surfaceType;
    public PlaceableObject.Type SurfaceType => _surfaceType;

    [SerializeField] private Material _tileMaterial;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _orientation;
    [SerializeField] private Vector3 _tileCount;
    [SerializeField] private Vector2 _tileScale;
    [SerializeField] private int _layer;

    private Mesh _tile;

    [SerializeField] private bool _isOn = true;
    public void SetIsOn(bool isOn) => _isOn = isOn;

    private void Awake()
    {
        _tile = new Mesh()
        {
            vertices = new Vector3[]
            {
                new(0, 0, 0),
                new(1, 0, 0),
                new(1, 1, 0),
                new(0, 1, 0)
            },
            triangles = new int[]
            {
                0, 1, 2,
                0, 2, 3
            },
            uv = new Vector2[]
            {
                new(0, 0),
                new(1, 0),
                new(1, 1),
                new(0, 1)
            }
        };

        _tile.RecalculateNormals();
    }

    private void Update()
    {
        DisplayGrid();
    }

    public void DisplayGrid()
    {
        if (!_isOn)
            return;

        for (int z = 0; z < _tileCount.z; ++z)
        {
            for (int y = 0; y < _tileCount.y; ++y)
            {
                for (int x = 0; x < _tileCount.x; ++x)
                {
                    Quaternion rotation = Quaternion.LookRotation(_orientation);
                    Matrix4x4 position = Matrix4x4.TRS(_offset + new Vector3(x, y, z), rotation, _tileScale);

                    Graphics.DrawMesh(_tile, position, _tileMaterial, _layer);
                }
            }
        }
    }
}
