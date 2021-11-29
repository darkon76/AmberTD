using System;
using UnityEngine;

public class BootStrapper : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnRuntimeMethodLoad()
    {
        var app = GameObject.Instantiate(Resources.Load("Prefabs/App")) as GameObject;
        if (app == null)
        {
            throw new ApplicationException("Can't find the App prefab");
        }

        app.name = "App";
        GameObject.DontDestroyOnLoad(app);
    }
}
