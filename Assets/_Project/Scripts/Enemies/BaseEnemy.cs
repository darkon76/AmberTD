using System.Collections;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    private const float DespawnTimer = .25f;

    private HealthHolder _health;
    private Collider _collider;
    private MoveToTarget _moveToTarget;

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
