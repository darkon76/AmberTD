using System;
using UnityEngine;

public class MoveToTarget : SOMover
{
    public override event Action OnTargetReached;
    
    // Update is called once per frame
    void Update()
    {
        if (Target == null)
        {
            return;
        }

        var targetPosition = Target.transform.position;
        var position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);

        transform.position = position;

        var distance = Vector3.SqrMagnitude(position -  targetPosition);
        if (distance < .0001f)
        {
            OnTargetReached?.Invoke();
        }
    }
}
