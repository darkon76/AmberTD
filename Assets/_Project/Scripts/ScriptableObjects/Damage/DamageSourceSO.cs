using UnityEngine;
public abstract class DamageSourceSO : ScriptableObject
{
    /// <summary>
    /// Creates the object that will do the damage. 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="damageOrigin"></param>
    public abstract void CreateOne(GameObject source,  GameObject target, Transform damageOrigin);

    /// <summary>
    /// The damage dealer reached its target, it is time to do the damage.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    public abstract void DealDamage(GameObject source, GameObject target);
}
