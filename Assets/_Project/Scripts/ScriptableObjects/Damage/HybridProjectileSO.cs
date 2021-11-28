using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Projectiles/HybridProjectileSO")]
public class HybridProjectileSO : ProjectileDamageSourceSO
{
    [SerializeField] private GameObject _prefab;

    public SingleTargetDamageDealer[] _singleTargetDamage;
    public AoeDamageDealer[] _aoeDamage;
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
        foreach (var damage in _singleTargetDamage)
        {
            damage.DealDamage(target);
        }
        foreach (var damage in _aoeDamage)
        {
            damage.DealDamage(target);
        }

    }
}
