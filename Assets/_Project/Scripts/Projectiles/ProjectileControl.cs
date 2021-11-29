using UnityEngine;

public class ProjectileControl : MonoBehaviour
{
    //TODO: Time
    private GameObject _hitEffect;
    
    //Just for debug it can be removed later.

    [SerializeField]
    private GameObject _target;
    [SerializeField]
    private SOMover _soMover;

    public ProjectileDamageSourceSO DamageSourceSo;

    public GameObject DamageSource;
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
        DamageSourceSo.DealDamage(DamageSource, _target);
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
        _soMover.Target = target;
    }

}
