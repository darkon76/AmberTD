using UnityEngine;

[System.Serializable]
public class SingleTargetDamageDealer : DamageDealer
{
    public override void DealDamage(GameObject source, GameObject target)
    {
        var health = target.GetComponent<HealthHolder>();
        if (health != null)
        {
            health.Current -= Damage;
        }
    }
}
