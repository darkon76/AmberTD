using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOAI : MonoBehaviour
{
    private const float DespawnTimer = .1f;

    private HealthHolder _health;
    private Collider _collider;
    private MoveToTarget _moveToTarget;

    [SerializeField]
    private GameObject DeadEffect;

    public int PointsWorth = 2;


    private void Awake()
    {
        if (_health == null)
        {
            _health = GetComponent<HealthHolder>();
        }

        _health.OnDead += Dead;
        _health.OnHealthChanged += Hurt;

        _collider = GetComponent<Collider>();
        _moveToTarget = GetComponent<MoveToTarget>();

    }

    private void OnEnable()
    {
        _moveToTarget.Target = LevelManager.Instance.EnemyObjective.gameObject;
        _health.Current = _health.Max;
        _collider.enabled = true;
        _moveToTarget.enabled = true;

    }

    private void OnDisable()
    {
        _moveToTarget.enabled = false;
        _collider.enabled = false;
    }

    public void Dead()
    {
        _moveToTarget.enabled = false;
        _collider.enabled = false;
        StartCoroutine(DeathRoutine());
        EventManager.Trigger(eEvent.PointsScored, PointsWorth);
        if (DeadEffect != null)
        {
            var deadSO = PoolManager.RequestGameObject(DeadEffect);
            deadSO.transform.position = _collider.bounds.center;
            deadSO.SetActive(true);
        }
    }

    public void Hurt()
    {
    }

    IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(DespawnTimer);
        //Return to the pool.
        gameObject.SetActive(false);
    }

}
