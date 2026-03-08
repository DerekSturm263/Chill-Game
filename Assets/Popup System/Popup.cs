using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Popup", menuName = "Popup System/Popup")]
public class Popup : ScriptableObject
{
    [SerializeField] private string _title;
    public string Title => _title;

    [SerializeField] private string _description;
    public string Description => _description;

    [SerializeField] private Sprite _image;
    public Sprite Image => _image;

    [SerializeField] private UnityButtonInfo[] _buttons;
    public ButtonInfo[] Buttons => _buttons.Select(item => new ButtonInfo()
    {
        label = item.label,
        onClick = () => item.onClick.Invoke()
    }).ToArray();
}
