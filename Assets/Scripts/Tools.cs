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

    public static bool CheckEqualWithThreshold(float value1, float value2, float thresholdInPersent)
    {
        float result = Mathf.Abs(value1 - value2);
        if (result < value1 * (thresholdInPersent / 100f))
            return true;
        return false;
    }
}