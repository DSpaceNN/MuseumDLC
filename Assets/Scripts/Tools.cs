using System.Collections.Generic;
using UnityEngine;

public static class Tools
{
    public static void DestroyAllChilds(Transform parent)
    {
        if (parent.childCount > 0)
        {
            var children = new List<GameObject>();
            foreach (Transform child in parent) children.Add(child.gameObject);
            children.ForEach(child => Object.Destroy(child));
        }
    }
}