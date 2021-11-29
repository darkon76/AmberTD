using System;
using UnityEngine;

public class BootStrapper : MonoBehaviour
{
    //Creates a gameobject will all the global managers.
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
