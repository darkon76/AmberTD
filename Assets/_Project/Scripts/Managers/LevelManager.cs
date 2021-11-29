using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance;

    public event Action OnKillAllEnemies;
    public EnemyObjective EnemyObjective { get; private set; }

    private void Awake()
    {
        Instance = this;
        EnemyObjective.OnStart += EnemyObjectiveCreated;
    }

    //It will never be called.
    private void OnDestroy()
    {
        EnemyObjective.OnStart -= EnemyObjectiveCreated;
    }
    private void EnemyObjectiveCreated(EnemyObjective enemyObjective)
    {
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
