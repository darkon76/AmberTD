using System;
using UnityEngine;

[System.Serializable]
public class MapHolder:MonoBehaviour
{
    public int Width = 30;
    public int Height = 30;

    public TileState[] Tiles;

    public static MapHolder Instance;

    
    private void Awake()
    {
        Instance = this;
        Tiles = new TileState[Width * Height];
        EnemyObjective.OnStart += EnemyObjectiveOnOnStart;
        EventManager.Listen(eEvent.LoadScene, OnLoadScene);
    }

    private void OnLoadScene()
    {
        for (int i = 0; i < Tiles.Length; i++)
        {
            Tiles[i] = TileState.Free;
        }
    }

    private void EnemyObjectiveOnOnStart(EnemyObjective obj)
    {
        var position = obj.transform.position;
        position.RoundToInt();
        TryPlace((int)position.x, (int)position.z, TileState.Blocked);
    }

    private void OnDestroy()
    {
        EnemyObjective.OnStart -= EnemyObjectiveOnOnStart;
        EventManager.Remove(eEvent.LoadScene, OnLoadScene);
    }


    public bool IsFree(int x, int z)
    {
        ref var currentTileState = ref Tiles[(x * Height) + z];
        return currentTileState == TileState.Free;
    }

    public bool TryPlace(int x, int z, TileState tileState)
    {
        ref var currentTileState = ref Tiles[(x * Height) + z];
        if (currentTileState != TileState.Free) 
            return false;
        currentTileState = tileState;
        return true;

    }

    public bool IsValid(Vector3 position)
    {
        var x = Mathf.RoundToInt(position.x);
        var z = Mathf.RoundToInt(position.z);
        return IsFree(x, z);
    }
}
