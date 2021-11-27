using UnityEngine;

public class MoveToTarget : SOMover
{
    public float Speed;
    
    // Update is called once per frame
    void Update()
    {
        if (Target == null)
        {
            return;
        }

        var position = Vector3.MoveTowards(transform.position, Target.transform.position, Speed * Time.deltaTime);

        transform.position = position;
    }
}
