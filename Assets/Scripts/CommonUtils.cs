using UnityEngine;
public static class CommonUtils
{
    public static float Distance2D(Vector3 a, Vector3 b)
    {
        return Vector2.Distance(new Vector2(a.x,a.y), new Vector2(b.x,b.y));
    }
}
