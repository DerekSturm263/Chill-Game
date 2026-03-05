using UnityEngine;

public static class UnityExtensionMethods
{
    public static Rect LerpRect(Rect rectA, Rect rectB, float t)
    {
        return new(Vector2.Lerp(rectA.position, rectB.position, t), Vector2.Lerp(rectA.size, rectB.size, t));
    }
}
