using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt2D : MonoBehaviour
{
    public GameObject Target;

    // Update is called once per frame
    void Update()
    {
        if (Target == null)
        {
            return;
        }

        var targetPos = Target.transform.position;
        targetPos.y = transform.position.y;
        transform.forward = targetPos - transform.position;
    }
}
