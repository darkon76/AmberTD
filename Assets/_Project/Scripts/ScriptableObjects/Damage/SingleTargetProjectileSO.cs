using UnityEngine;

//Shoots a single projectile and does single target damage on reaching its destination.
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Projectiles/SingleTargetProjectileSO")]
public class SingleTargetProjectileSO : ProjectileDamageSourceSO
{
    [SerializeField] private GameObject _prefab;

    public SingleTargetDamageDealer _damage;

    public override void CreateOne(GameObject source, GameObject target, Transform damageOrigin)
    {
        //Get an object from the pool.
        var projectileGO = PoolManager.RequestGameObject(_prefab, 4);
        var projectile = projectileGO.GetComponent<ProjectileControl>();
        //Set the projectile position and orientation to match the launcher origin.
        projectile.transform.SetPositionAndRotation(damageOrigin.position, damageOrigin.rotation);
        
        //There can be multiple projectiles using the same prefab so a registration is needed.
        projectile.DamageSourceSo = this;
        projectile.SetTarget(target);
        projectile.DamageSource = source;

        //Activate the projectile. 
        projectileGO.SetActive(true);
    }

    public override void DealDamage(GameObject source, GameObject target)
    {
        _damage.DealDamage(source, target);
    }
}
