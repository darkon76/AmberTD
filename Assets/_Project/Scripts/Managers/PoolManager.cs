using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A wrapper for multiple object pools for easy access and control.
/// TODO: Clear the pools when reaching the main menu.
/// </summary>
public static class PoolManager
{
    private static GameObject poolHolder;
    private static Dictionary<GameObject, PoolObjectContainer> _pools = new Dictionary<GameObject, PoolObjectContainer>();

    static PoolManager()
    {
        EventManager.Listen(eEvent.LoadScene, OnLoadScene);
    }

    private static void OnLoadScene()
    {
        for (var i = 0; i < poolHolder.transform.childCount; i++)
        {
            var child = poolHolder.transform.GetChild(i);
            child.gameObject.SetActive(false);
        }
    }

    public static GameObject RequestGameObject(GameObject gameObject, int count = 1)
    {
        if (poolHolder == null)
        {
            poolHolder = new GameObject("__PoolHolder");
            GameObject.DontDestroyOnLoad(poolHolder);
        }

        if (!_pools.TryGetValue(gameObject, out var pool))
        {
            pool = new PoolObjectContainer(gameObject, poolHolder.transform, count);
            _pools[gameObject] = pool;
        }

        var go =pool.Get();
        return go;
    }

    public static void Clear()
    {
        _pools.Clear();
    }
    

}
