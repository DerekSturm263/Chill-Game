using UnityEngine;

[CreateAssetMenu(fileName = "New Wallpaper", menuName = "Scriptable Objects/Wallpaper")]
public class Wallpaper : Asset
{
    [SerializeField] private Texture2D _texture;
    public Texture2D Texture => _texture;
}
