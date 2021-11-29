using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Projectiles/AoeProjectileSO")]
public class AoeProjectileSO : ProjectileDamageSourceSO
{
    [SerializeField] private GameObject _prefab;

    public AoeDamageDealer _damage;
    public override void CreateOne(GameObject source, GameObject target,  Transform damageOrigin)
    {
        var projectileGO = PoolManager.RequestGameObject(_prefab, 4);
        var projectile = projectileGO.GetComponent<ProjectileControl>();
        projectile.transform.SetPositionAndRotation(damageOrigin.transform.position, damageOrigin.transform.rotation);
        projectile.DamageSourceSo = this;
        projectile.DamageSource = source;
        projectile.SetTarget(target);
        projectileGO.SetActive(true);
    }

    public override void DealDamage(GameObject source, GameObject target)
    {
        _damage.DealDamage(source, target);
    }
}
