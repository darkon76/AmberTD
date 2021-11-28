using System.Collections.Generic;
using UnityEngine;

public static class PoolManager
{
    private static GameObject poolHolder;
    private static Dictionary<GameObject, PoolObjectContainer> _pools = new Dictionary<GameObject, PoolObjectContainer>();
    
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
