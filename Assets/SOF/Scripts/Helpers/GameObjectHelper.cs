using UnityEngine;
using UnityEditor;

public static class GameObjectHelper
{
    public static void RemoveComponent<T>(this GameObject go) where T: Component
    {
        GameObject.DestroyImmediate(go.GetComponent<T>());
    }
}