using UnityEngine;

public class Movement
{
    public Vector3 BasePosition { get; set; }
    public Vector2 Direction { get; set; }

    public Vector3 GetDestination(int spaces)
    {
        return new Vector3(
            BasePosition.x + (Direction.x * spaces),
            BasePosition.y,
            BasePosition.z + (Direction.y * spaces));
    }
}