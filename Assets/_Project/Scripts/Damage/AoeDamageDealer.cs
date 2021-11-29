using UnityEngine;

/// <summary>
/// Deal damage in an area.
/// </summary>
[System.Serializable]
public class AoeDamageDealer : DamageDealer
{
    /// <summary>
    /// The aoe radius.
    /// </summary>
    [SerializeField]
    private float _radius;
    /// <summary>
    /// The layer used to search for the objects that will be damaged.
    /// </summary>
    [SerializeField]
    private LayerMask _layerMask;
    public override void DealDamage(GameObject source, GameObject target)
    {
        //Because we don't know how many targets can be hit, with some testing the noalloc is better. 
        var colliders = Physics.OverlapSphere(target.transform.position, _radius, _layerMask);
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
