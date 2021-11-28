using System.Collections;
using Pathfinding;
using UnityEngine;
using UnityEngine.Windows.WebCam;

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
    private Rigidbody _rigidbody;

    private IEnumerator _deathRoutine;



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
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        //Restore values.
        _rigidbody.isKinematic = false;
        _rigidbody.velocity = Vector3.zero;
        _health.Current = _health.Max;
        _collider.enabled = true;
        _canBeHurt = true;
        _animator.ResetTrigger(HurtHash);
        _animator.ResetTrigger(DeathHash);
        _animator.SetFloat(WalkingSpeedHash, 0);
        _aiPath.canMove = true;
    }

    private void Update()
    {
        Vector3 relVelocity = transform.InverseTransformDirection(_aiPath.velocity);
        relVelocity.y = 0;

        // Speed relative to the character size
        _animator.SetFloat(WalkingSpeedHash, (relVelocity.magnitude / .5f) / _aiPath.transform.lossyScale.x);
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
        _rigidbody.isKinematic = true;
        _collider.enabled = false;
        _animator.SetTrigger(DeathHash);
        _canBeHurt = false;
        _aiPath.canMove = false;
        _deathRoutine = DeathRoutine();
        StartCoroutine(_deathRoutine);
    }

    public void Hurt()
    {
        if (!_canBeHurt)
        {
            return;
        }
        _animator.SetTrigger(HurtHash);
    }

    IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(DespawnTimer);
        //Return to the pool.
        gameObject.SetActive(false);
    }
}
