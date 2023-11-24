using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtension
{
    public static List<GameObject> GetChildren(this GameObject gameObject)
    {
        List<GameObject> children = new List<GameObject>();
        if (gameObject != null || gameObject.transform.childCount == 0) return children;

        foreach (Transform t in gameObject.transform)
        {
            children.Add(t.gameObject);
        }
        return children;
    }
}
