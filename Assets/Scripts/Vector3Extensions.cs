using UnityEngine;

public static class Vector3Extensions
{
    public static Vector3 AddY(this Vector3 vector, float term)
    {
        return new Vector3(vector.x, vector.y + term, vector.z);
    }
}