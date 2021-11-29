using System;
using UnityEngine;

public class EnemyObjective : MonoBehaviour
{

    private HealthHolder _health;

    private void Awake()
    {
        _health = GetComponent<HealthHolder>();
        _health.OnDead += HealthOnDead;
    }

    private void HealthOnDead()
    {
        EventManager.Trigger(eEvent.PlayerLose);
    }

    // Start is called before the first frame update
    private void Start()
    {
        //Register to the level manager
        EventManager.Trigger(eEvent.EnemyObjectCreated, this);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
        _health.Current--;
    }

}
