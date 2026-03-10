using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Music", menuName = "Scriptable Objects/Music")]
public class Music : Asset
{
    [SerializeField] private AudioClip _clip;

    public override void OnTapObject(GameObject obj)
    {

    }

    public override void OnHoldObject(GameObject obj)
    {

    }
}
