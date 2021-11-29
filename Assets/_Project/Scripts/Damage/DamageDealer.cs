using UnityEngine;

[System.Serializable]
public abstract class DamageDealer
{
    public float Damage;

    public abstract void DealDamage(GameObject source, GameObject target);
}
