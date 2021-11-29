using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public PoolObjectContainer Containter;
    //When the object is disabled return it to the pool.
    private void OnDisable()
    {
        if (Containter != null)
            Containter.ReturnToPool(this);
    }
}