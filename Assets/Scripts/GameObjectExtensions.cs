using System;
using System.Collections.Generic;
using System.Linq;
using Behaviours;
using UnityEngine;

public static class GameObjectExtensions
{
    public static T GetLinkedComponent<T>(this GameObject gameObject)
        where T : Component
    {
        if (gameObject == null)
        {
            return default;
        }

        var linkedObjectComponents = gameObject.GetComponents<LinkedGameObject>();

        var requestedComponents = new List<T>();

        foreach (var linkedObjectComponent in linkedObjectComponents)
        {
            var result = linkedObjectComponent.linkedObject.TryGetComponent(typeof(T), out var component);

            if (result)
            {
                requestedComponents.Add((T)component);
            }
        }

        if (requestedComponents.Count > 1)
        {
            throw new InvalidOperationException($"Multiple linked components of type {typeof(T)}");
        }


        return requestedComponents.Single();
    }
}