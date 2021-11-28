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

    private Collider _targetCollider;

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
        //Check if the target is still valid.
        if (_targetCollider != null && _targetCollider.enabled)
        {
            //The target is still at range.
            var sqrMagnitude = Vector3.SqrMagnitude(Target.transform.position - transform.position);
            if (sqrMagnitude <= _sqrRadius)
            {
                
                return;
            }
        }

        //Using physics isn't optimal but for time limitations is the best way. 
        var numColliders =  Physics.OverlapSphereNonAlloc(transform.position, _radius, _colliders, _layer);

        //Evaluates all the colliders to get the best target.
        Collider bestTarget = null;
        var minValue = float.MaxValue;
        for (var i = 0; i < numColliders; i++)
        {
            var collider = _colliders[i];
            var value = _targetEvaluator.Evaluate(transform, collider.gameObject);
            if (value < minValue)
            {
                minValue = value;
                bestTarget = collider;
            }
        }
        _targetCollider = bestTarget;
        Target = bestTarget?.gameObject;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Target == null? Color.green: Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
        if (Target != null)
        {
            Gizmos.DrawLine(transform.position, Target.transform.position);
        }
    }

    
}
