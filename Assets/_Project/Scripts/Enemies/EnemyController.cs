using System.Collections;
using Pathfinding;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private const float DespawnTimer = 10f;
    
    private Animator _animator;
    public static int HurtHash = Animator.StringToHash("Hurt");
    public static int DeathHash = Animator.StringToHash("Death");
    public static int WalkingSpeedHash = Animator.StringToHash("WalkingSpeed");

    private HealthHolder _health;
    private Collider _collider;

    private bool _canBeHurt = true;

    public AIPath _aiPath;
    private AIDestinationSetter _aiDestinationSetter;

    private IEnumerator _deathRoutine;


    public float Speed = .5f;
    public float MaxSpeed = 1;

    public int PointsWorth = 1;


    private void Awake()
    {
        if (_animator == null)
        {
            _animator = GetComponentInChildren<Animator>();
        }

        if (_health == null)
        {
            _health = GetComponent<HealthHolder>();
        }

        _health.OnDead += Dead;
        _health.OnHealthChanged += Hurt;

        if (_collider == null)
        {
            _collider = GetComponent<Collider>();
        }

        _aiPath = GetComponent<AIPath>();
        _aiDestinationSetter = GetComponent<AIDestinationSetter>();
    }

    private void Start()
    {
        if (_aiDestinationSetter)
        {
            _aiDestinationSetter.target = LevelManager.Instance.EnemyObjective.transform;
        }
    }

    private void OnEnable()
    {
        //Restore values.
        _health.Current = _health.Max;
        _collider.enabled = true;
        _canBeHurt = true;
        if (_animator)
        {
            _animator.ResetTrigger(HurtHash);
            _animator.ResetTrigger(DeathHash);
            _animator.SetFloat(WalkingSpeedHash, 0);
        }

        if (_aiPath)
        {
            _aiPath.canMove = true;
        }

        _aiPath.maxSpeed = Speed;
    }

    private void Update()
    {
        if (_animator)
        {
            Vector3 relVelocity = transform.InverseTransformDirection(_aiPath.velocity);
            relVelocity.y = 0;

            // Speed relative to the character size
            _animator.SetFloat(WalkingSpeedHash, Speed / MaxSpeed);
        }

    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnDestroy()
    {
        _health.OnDead -= Dead;
    }

    public void Dead()
    {
        _collider.enabled = false;
        if (_animator)
        {
            _animator.SetTrigger(DeathHash);
        }
        _canBeHurt = false;
        if (_aiPath)
        {
            _aiPath.canMove = false;
        }
        EventManager.Trigger(eEvent.PointsScored, PointsWorth);
        _deathRoutine = DeathRoutine();
        StartCoroutine(_deathRoutine);
    }

    public void Hurt()
    {
        if (!_canBeHurt)
        {
            return;
        }

        if (_animator)
        {
            _animator.SetTrigger(HurtHash);
        }
    }

    IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(DespawnTimer);
        //Return to the pool.
        gameObject.SetActive(false);
    }
}
