using UnityEngine;

public class RoomInitializer : MonoBehaviour
{
    [SerializeField] private PlaceableSurface _floor;
    [SerializeField] private PlaceableSurface _ceiling;
    [SerializeField] private PlaceableSurface _leftWall;
    [SerializeField] private PlaceableSurface _rightWall;
    [SerializeField] private PlaceableSurface _forwardWall;
    [SerializeField] private PlaceableSurface _backWall;

    public void OnLoad()
    {
        var dimensions = SaveMethods.Current.roomData.roomDimensions;
        var color = SaveMethods.Current.roomData.wallpaperColor;
        var wallpaper = SaveMethods.Current.roomData.wallpaper;

        Vector3 floorCeilingDimensions = dimensions;
        floorCeilingDimensions.y = 10;

        _floor.transform.position = new Vector3(0, dimensions.y / -2f, 0);
        _floor.transform.localScale = floorCeilingDimensions / 10;
        _floor.TileCount.x = dimensions.x;
        _floor.TileCount.z = dimensions.z;
        _floor.Offset.x = dimensions.x / -2f + 0.05f;
        _floor.Offset.z = dimensions.z / -2f + 1 - 0.05f;

        _ceiling.transform.position = new Vector3(0, dimensions.y / 2f, 0);
        _ceiling.transform.localScale = floorCeilingDimensions / 10;
        _ceiling.TileCount.x = dimensions.x;
        _ceiling.TileCount.z = dimensions.z;
        _ceiling.Offset.x = dimensions.x / -2f + 0.05f;
        _ceiling.Offset.z = dimensions.z / -2f + 0.05f;
        if (_ceiling.TryGetComponent(out MeshRenderer rendererTop))
        {
            rendererTop.material.SetColor("_BaseColor", color);
            rendererTop.material.SetTexture("_MainTex", wallpaper.Texture);
        }



        Vector3 wallLeftRightDimensions = dimensions;
        wallLeftRightDimensions.x = dimensions.z;
        wallLeftRightDimensions.y = 10;
        wallLeftRightDimensions.z = dimensions.y;

        _leftWall.transform.position = new Vector3(dimensions.x / -2f, 0, 0);
        _leftWall.transform.localScale = wallLeftRightDimensions / 10;
        _leftWall.TileCount.y = dimensions.y;
        _leftWall.TileCount.z = dimensions.z;
        _leftWall.Offset.y = dimensions.y / -2f + 0.05f;
        _leftWall.Offset.z = dimensions.z / -2f + 1 - 0.05f;
        if (_leftWall.TryGetComponent(out MeshRenderer rendererLeft))
        {
            rendererLeft.material.SetColor("_BaseColor", color);
            rendererLeft.material.SetTexture("_MainTex", wallpaper.Texture);
        }

        _rightWall.transform.position = new Vector3(dimensions.x / 2f, 0, 0);
        _rightWall.transform.localScale = wallLeftRightDimensions / 10;
        _rightWall.TileCount.y = dimensions.y;
        _rightWall.TileCount.z = dimensions.z;
        _rightWall.Offset.y = dimensions.y / -2f + 0.05f;
        _rightWall.Offset.z = dimensions.z / -2f + 0.05f;
        if (_rightWall.TryGetComponent(out MeshRenderer rendererRight))
        {
            rendererRight.material.SetColor("_BaseColor", color);
            rendererRight.material.SetTexture("_MainTex", wallpaper.Texture);
        }



        Vector3 wallForwardBackDimensions = dimensions;
        wallForwardBackDimensions.y = 10;
        wallForwardBackDimensions.z = dimensions.y;

        _forwardWall.transform.position = new Vector3(0, 0, dimensions.z / 2f);
        _forwardWall.transform.localScale = wallForwardBackDimensions / 10;
        _forwardWall.TileCount.x = dimensions.x;
        _forwardWall.TileCount.y = dimensions.y;
        _forwardWall.Offset.x = dimensions.x / -2f + 1 - 0.05f;
        _forwardWall.Offset.y = dimensions.y / -2f + 0.05f;
        if (_forwardWall.TryGetComponent(out MeshRenderer rendererForward))
        {
            rendererForward.material.SetColor("_BaseColor", color);
            rendererForward.material.SetTexture("_MainTex", wallpaper.Texture);
        }

        _backWall.transform.position = new Vector3(0, 0, dimensions.z / -2f);
        _backWall.transform.localScale = wallForwardBackDimensions / 10;
        _backWall.TileCount.x = dimensions.x;
        _backWall.TileCount.y = dimensions.y;
        _backWall.Offset.x = dimensions.x / -2f + 0.05f;
        _backWall.Offset.y = dimensions.y / -2f + 0.05f;
        if (_backWall.TryGetComponent(out MeshRenderer rendererBack))
        {
            rendererBack.material.SetColor("_BaseColor", color);
            rendererBack.material.SetTexture("_MainTex", wallpaper.Texture);
        }
    }
}
