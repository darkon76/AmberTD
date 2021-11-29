using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    public bool CanSpawn = true;
    private void Awake()
    {
        EventManager.Listen(eEvent.PlayerLose, OnPlayerLose);
    }

    private void OnDestroy()
    {
        EventManager.Remove(eEvent.PlayerLose, OnPlayerLose);
    }

    private void OnPlayerLose()
    {
        //Stop the spawning
        Destroy(gameObject);
    }

}
