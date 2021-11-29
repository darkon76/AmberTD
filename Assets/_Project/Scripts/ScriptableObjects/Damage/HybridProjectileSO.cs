using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Projectiles/HybridProjectileSO")]
public class HybridProjectileSO : ProjectileDamageSourceSO
{
    [SerializeField] private GameObject _prefab;

    //Can do single and aoe damage
    //Array for the future because there can be different damage types, like fire, pierce etc.
    public SingleTargetDamageDealer[] _singleTargetDamage;
    public AoeDamageDealer[] _aoeDamage;
    public override void CreateOne(GameObject source, GameObject target, Transform damageOrigin)
    {
        var projectileGO = PoolManager.RequestGameObject(_prefab, 4);
        var projectile = projectileGO.GetComponent<ProjectileControl>();
        projectile.transform.SetPositionAndRotation(damageOrigin.position, damageOrigin.rotation);
        projectile.DamageSourceSo = this;
        projectile.DamageSource = source;
        projectile.SetTarget(target);
        projectileGO.SetActive(true);
    }

    public override void DealDamage(GameObject source, GameObject target)
    {
        foreach (var damage in _singleTargetDamage)
        {
            damage.DealDamage(source, target);
        }
        foreach (var damage in _aoeDamage)
        {
            damage.DealDamage(source, target);
        }

    }
}
