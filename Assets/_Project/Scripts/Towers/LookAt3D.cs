using UnityEngine;

public class LookAt3D : MonoBehaviour
{
    private GameObject _target;
    private Vector3 _offset;
    public GameObject Target
    {
        get => _target;
        set
        {
            _target = value;
            if (_target == null)
            {
                return;
            }
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

        transform.LookAt(Target.transform.position + _offset, Vector3.up);
    }
}
