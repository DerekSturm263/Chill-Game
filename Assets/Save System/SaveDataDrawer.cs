#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(SaveData))]
public class SaveDataDrawer : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        VisualElement container = new();

        SerializedProperty iterator = property.Copy();
        SerializedProperty end = iterator.GetEndProperty();
 
        if (iterator.Next(true))
        {
            while (!SerializedProperty.EqualContents(iterator, end))
            {
                container.Add(new PropertyField(iterator));
                iterator.Next(false);
            }
        }

        return container;
    }
}

#endif