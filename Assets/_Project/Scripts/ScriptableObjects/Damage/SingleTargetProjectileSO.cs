using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Projectiles/SingleTargetProjectileSO")]
public class SingleTargetProjectileSO : ProjectileDamageSourceSO
{
    [SerializeField] public float Speed
#if UNITY_EDITOR //Micro optimization most of the time doesn't worth it, it will be the only place that it will happen. 
    { get; private set; }
#else 
    ;
#endif
    [SerializeField] private GameObject _prefab;

    public SingleTargetDamageDealer _damage;
    public override void CreateOne(Transform source, Transform target)
    {
        var projectileGO = PoolManager.RequestGameObject(_prefab, 4);
        var projectile = projectileGO.GetComponent<ProjectileControl>();
        projectile.transform.SetPositionAndRotation(source.transform.position, source.transform.rotation);
        projectile.DamageSourceSo = this;
        projectile.SetTarget(target.transform);
        projectileGO.SetActive(true);
    }

    public override void DealDamage(Transform target)
    {
        _damage.DealDamage(target);
    }
}
