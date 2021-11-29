using UnityEngine;

public interface ITargetEvaluator
{
    float Evaluate(Transform referenceTransform, GameObject targetGameObject);
}
