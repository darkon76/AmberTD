using UnityEngine;

//Shoots a single projectile and does single target damage on reaching its destination.
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Projectiles/SingleTargetProjectileSO")]
public class SingleTargetProjectileSO : ProjectileDamageSourceSO
{
    [SerializeField] private GameObject _prefab;

    public SingleTargetDamageDealer _damage;

    public override void CreateOne(Transform source, Transform target)
    {
        //Get an object from the pool.
        var projectileGO = PoolManager.RequestGameObject(_prefab, 4);
        var projectile = projectileGO.GetComponent<ProjectileControl>();
        //Set the projectile position and orientation to match the launcher origin.
        projectile.transform.SetPositionAndRotation(source.transform.position, source.transform.rotation);
        
        //There can be multiple projectiles using the same prefab so a registration is needed.
        projectile.DamageSourceSo = this;
        projectile.SetTarget(target.transform);

        //Activate the projectile. 
        projectileGO.SetActive(true);
    }

    public override void DealDamage(Transform target)
    {
        _damage.DealDamage(target);
    }
}
