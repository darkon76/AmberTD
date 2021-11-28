using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Projectiles/AoeProjectileSO")]
public class AoeProjectileSO : ProjectileDamageSourceSO
{
    [SerializeField] private GameObject _prefab;

    public AoeDamageDealer _damage;
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
