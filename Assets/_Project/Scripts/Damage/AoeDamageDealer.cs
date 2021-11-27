using UnityEngine;

public class AoeDamageDealer : DamageDealer
{
    [SerializeField]
    private float _radius;
    [SerializeField]
    private LayerMask _layerMask;
    public override void DealDamage(Transform target)
    {
        //Because we don't know how many targets can be hit, with some testing the noalloc is better. 
        var colliders = Physics.OverlapSphere(target.position, _radius, _layerMask);
        foreach (var collider in colliders)
        {
            var health = collider.gameObject.GetComponent<HealthHolder>();
            if (health != null)
            {
                health.Current -= Damage;
            }
        }
    }
}
