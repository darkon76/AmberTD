using UnityEngine;
public abstract class DamageSourceSO : ScriptableObject
{
    public abstract void CreateOne(GameObject source,  GameObject target, Transform damageOrigin);

    public abstract void DealDamage(GameObject source, GameObject target);
}
