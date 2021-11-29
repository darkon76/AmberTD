using System;
using UnityEngine;

public class MoveToTarget : SOMover
{
    public override event Action OnTargetReached;
    [SerializeField]
    private GameObject _target;

    private Vector3 _offset;
    public override GameObject Target
    {
        get => _target;
        set
        {
            _target = value;
            _offset = Vector3.zero;
            if (_target.GetComponent<TargetCollider>() == null)
            {
                return;
            }

            var collider = _target.GetComponent<Collider>();
            if (collider == null)
            {
                return;
            }

            var center = collider.bounds.center;

            _offset = center - _target.transform.position;

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null)
        {
            return;
        }

        var targetPosition = Target.transform.position +  _offset;
        var position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);

        transform.position = position;

        var distance = Vector3.SqrMagnitude(position -  targetPosition);
        if (distance < .0001f)
        {
            OnTargetReached?.Invoke();
        }
    }
}
