using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Conversions
{
    public static Vector3 ToCustomVector3(this Vector2 vec2)
    {
        return new Vector3(vec2.x, 0f, vec2.y);
    }
}
