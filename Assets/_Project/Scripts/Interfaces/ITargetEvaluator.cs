using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetEvaluator
{
    float Evaluate(Transform referenceTransform, GameObject targetGameObject);
}
