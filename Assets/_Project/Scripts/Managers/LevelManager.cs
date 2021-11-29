using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance;

    public event Action OnKillAllEnemies;
    public EnemyObjective EnemyObjective { get; private set; }

    private void Awake()
    {
        Instance = this;
        EventManager.Listen(eEvent.EnemyObjectCreated, EnemyObjectiveCreated);
    }

    //It will never be called.
    private void OnDestroy()
    {
        EventManager.Remove(eEvent.EnemyObjectCreated, EnemyObjectiveCreated);
    }
    private void EnemyObjectiveCreated(object[] parameters)
    {
        var enemyObjective = parameters[0] as EnemyObjective;
        UnRegisterEnemyObjective();
        EnemyObjective = enemyObjective;
    }

    private void UnRegisterEnemyObjective()
    {
        if (EnemyObjective == null)
        {
            return;
        }
        EnemyObjective = null;
    }

    public void RegisterEnemyObjective(EnemyObjective enemyObjective)
    {
        EnemyObjective = enemyObjective;
    }

    [ContextMenu("Restart level")]
    private void RestartLevel()
    {
        UnRegisterEnemyObjective();
        ProjectSceneManager.Instance.Restart();
    }

    [ContextMenu("Kill all enemies")]
    private void KillAllEnemies()
    {
        OnKillAllEnemies?.Invoke();
    }
}
