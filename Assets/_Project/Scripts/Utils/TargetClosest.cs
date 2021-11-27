using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetClosest : ITargetEvaluator
{
    public float Evaluate(Transform referenceTransform, GameObject targetGameObject)
    {
        var sqrMagnitude = Vector3.SqrMagnitude(targetGameObject.transform.position - referenceTransform.position);
        return sqrMagnitude;
    }
}
