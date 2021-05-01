using Behaviours;
using UnityEngine;

public static class GameObjectExtensions
{
    public static T GetLinkedComponent<T>(this GameObject gameObject)
    {
        if (gameObject == null)
        {
            return default;
        }

        var linkedObject =  gameObject.GetComponent<LinkedGameObject>()?.linkedObject;
        return linkedObject.GetComponent<T>();
    }
}