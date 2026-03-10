using UnityEngine;

public static class UnityExtensionMethods
{
    public static Rect LerpRect(Rect rectA, Rect rectB, float t)
    {
        return new(Vector2.Lerp(rectA.position, rectB.position, t), Vector2.Lerp(rectA.size, rectB.size, t));
    }

    public static GameObject FindChildByTag(this GameObject gameObject, string tag)
    {
        foreach (var child in gameObject.GetComponentsInChildren<Transform>())
        {
            if (child.CompareTag(tag))
                return child.gameObject;
        }

        return null;
    }

    public static bool TryFindChildByTag(this GameObject gameObject, string tag, out GameObject childWithTag)
    {
        childWithTag = gameObject.FindChildByTag(tag);
        
        return childWithTag != null;
    }
}
