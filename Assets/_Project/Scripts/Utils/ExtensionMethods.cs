using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static void RoundToInt(this ref Vector3 position)
    {
        position.x = Mathf.RoundToInt(position.x);
        position.y = Mathf.RoundToInt(position.y);
        position.z = Mathf.RoundToInt(position.z);
    }
}
