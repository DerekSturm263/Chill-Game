using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public struct ButtonInfo
{
    public string label;
    public Action onClick;
}

public struct UnityButtonInfo
{
    public string label;
    public UnityEvent onClick;

    public static implicit operator ButtonInfo(UnityButtonInfo src) => new()
    {
        label = src.label,
        onClick = src.onClick.Invoke
    };
}

public class PopupSystem : ScriptableObject
{
    private static readonly string[] AssetPath = { "Settings", "Popup System", "Popup System.asset" };

    [SerializeField][Tooltip("The GameObject to spawn when creating a popup. This should contain children with the tags: \"Title\", \"Description\", \"Image\", and \"Buttons\".")] private GameObject _popup;
    [SerializeField][Tooltip("The Button to spawn if a popup has any ButtonInfos assigned.")] private Button _button;

#if UNITY_EDITOR

    public static PopupSystem Instance
    {
        get
        {
            if (!UnityEditor.AssetDatabase.IsValidFolder($"Assets/{AssetPath[0]}"))
                UnityEditor.AssetDatabase.CreateFolder("Assets", AssetPath[0]);

            if (!UnityEditor.AssetDatabase.IsValidFolder($"Assets/{AssetPath[0]}/{AssetPath[1]}"))
                UnityEditor.AssetDatabase.CreateFolder($"Assets/{AssetPath[0]}", AssetPath[1]);

            string fullPath = Path.Combine("Assets", AssetPath[0], AssetPath[1], AssetPath[2]);
            PopupSystem settings = UnityEditor.AssetDatabase.LoadAssetAtPath<PopupSystem>(fullPath);

            if (!settings)
            {
                settings = CreateInstance<PopupSystem>();

                UnityEditor.AssetDatabase.CreateAsset(settings, fullPath);
                UnityEditor.AssetDatabase.SaveAssets();
            }

            return settings;
        }
    }

    internal static UnityEditor.SerializedObject GetSerializedObject() => new(Instance);

#endif

    [RuntimeInitializeOnLoadMethod]
    private static void OnStartup() => Instance.Startup();

    private void Startup()
    {

        Application.quitting += () =>
        {

        };
    }

    internal void Display(string title, string description, Sprite image, ButtonInfo[] buttons)
    {
        GameObject popupInstance = Instantiate(_popup);
        popupInstance.name = $"{title} Popup";

        popupInstance.FindChildByTag("Title")?.GetComponent<TMPro.TMP_Text>()?.SetText(title);
        popupInstance.FindChildByTag("Description")?.GetComponent<TMPro.TMP_Text>().SetText(description);

        Image imageComponent = popupInstance.FindChildByTag("Image")?.GetComponent<Image>();
        if (imageComponent)
            imageComponent.sprite = image;

        GameObject buttonsParent = popupInstance.FindChildByTag("Buttons");
        if (buttonsParent)
        {
            foreach (ButtonInfo button in buttons)
            {
                Button buttonInstance = Instantiate(_button, buttonsParent.transform);

                buttonInstance.onClick.AddListener(button.onClick.Invoke);
                buttonInstance.GetComponentInChildren<TMPro.TMP_Text>().SetText(button.label);
            }
        }
    }
}
