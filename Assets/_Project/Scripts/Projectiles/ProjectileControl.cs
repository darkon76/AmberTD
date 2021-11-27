using UnityEngine;

public class ProjectileControl : MonoBehaviour
{

    private GameObject _hitEffect;
    
    //Just for debug it can be removed later.
#if UNITY_EDITOR
    [SerializeField]
    private Transform _target;
#endif
    [SerializeField]
    private SOMover _soMover;

    public ProjectileDamageSourceSO DamageSourceSo;


    private void Awake()
    {
        if (_soMover == null)
        {
            _soMover = GetComponent<SOMover>();
        }
        _soMover.OnTargetReached += TargetReached;
    }

    private void OnEnable()
    {
        if (DamageSourceSo)
        {
            _soMover.Speed = DamageSourceSo.Speed;
        }
    }

    private void OnDestroy()
    {
        _soMover.OnTargetReached -= TargetReached;
    }

    private void TargetReached()
    {
        gameObject.SetActive(false); //Because it is in a pool we return it. 

        if (_hitEffect != null)
        {
            var hitEffect = PoolManager.RequestGameObject(_hitEffect);
            hitEffect.transform.position = transform.position;
            hitEffect.SetActive(true);
        }
        DamageSourceSo.DealDamage( _target);
    }

    public void SetTarget(Transform target)
    {
#if UNITY_EDITOR
        _target = target;
#endif
        _soMover.Target = target;
    }

}
