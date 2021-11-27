using System;
using UnityEngine;

public class Targeting : MonoBehaviour
{

    [SerializeField]
    private GameObject _target;

    public GameObject Target
    {
        get => _target;
        private set
        {
            if (value == _target)
            {
                return;
            }

            _target = value;

            OnTargetChanged?.Invoke();
        }
    }

    public event Action OnTargetChanged;

    [SerializeField]
    private float _radius;
    private float _sqrRadius;

    [SerializeField]
    private LayerMask _layer;

    private ITargetEvaluator _targetEvaluator = new TargetClosest();
    
    private Collider[] _colliders = new Collider[5];

    void OnValidate()
    {
        _sqrRadius = _radius  * _radius;
    }

    void Awake()
    {
        _sqrRadius = _radius * _radius;
    }

    public void SetEvaluator(ITargetEvaluator targetEvaluator)
    {
        _targetEvaluator = targetEvaluator;
    }

    public void SetRadius(float radius)
    {
        _radius = radius;
        _sqrRadius = radius * radius;
    }

    void Update()
    {
        if (Target != null && Target.activeSelf)
        {
            var sqrMagnitude = Vector3.SqrMagnitude(Target.transform.position - transform.position);
            if (sqrMagnitude <= _sqrRadius)
            {
                return;
            }
        }

        //Using physics isn't optimal but for time limitations is the best way. 
        var numColliders =  Physics.OverlapSphereNonAlloc(transform.position, _radius, _colliders, _layer);

        GameObject bestTarget = null;
        var minValue = float.MaxValue;
        for (var i = 0; i < numColliders; i++)
        {
            var colliderGO = _colliders[i].gameObject;
            var value = _targetEvaluator.Evaluate(transform, colliderGO);
            if (value < minValue)
            {
                minValue = value;
                bestTarget = colliderGO;
            }
        }

        Target = bestTarget;

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    
}
