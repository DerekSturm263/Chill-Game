using UnityEngine;

[CreateAssetMenu(fileName = "Music", menuName = "Scriptable Objects/Music")]
public class Music : Asset
{
    [SerializeField] private AudioClip _clip;
}
