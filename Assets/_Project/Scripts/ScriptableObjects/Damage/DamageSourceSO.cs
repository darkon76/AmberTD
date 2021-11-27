using UnityEngine;
public abstract class DamageSourceSO : ScriptableObject
{
    public abstract void CreateOne(Transform source, Transform target);

    public abstract void DealDamage(Transform target);
}
