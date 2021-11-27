using UnityEngine;

public class ProjectileControl : MonoBehaviour
{

    //Just for debug it can be removed later.
#if UNITY_EDITOR
    [SerializeField]
    private Transform _target;
#endif
    [SerializeField]
    private SOMover _soMover;

    private void Awake()
    {
        if (_soMover == null)
        {
            _soMover = GetComponent<SOMover>();
        }
    }

    public void SetTarget(Transform target)
    {
#if UNITY_EDITOR
        _target = target;
#endif
        _soMover.Target = target;
    }

}
