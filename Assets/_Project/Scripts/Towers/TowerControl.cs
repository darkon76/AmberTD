using UnityEngine;

//Tower Control link all the components.
public class TowerControl : MonoBehaviour
{
    private LookAt2D _turrentLookAt2D;
    private LookAt3D _turrentLookAt3D;
    private Targeting _targeting;
    private ProjectileLauncher _turretProjectileLauncher;

    private void Awake()
    {
        if (_targeting == null)
        {
            _targeting = GetComponent<Targeting>();
        }

        if (_turrentLookAt2D == null)
        {
            _turrentLookAt2D = GetComponentInChildren<LookAt2D>();
        }

        if (_turrentLookAt3D == null)
        {
            _turrentLookAt3D = GetComponentInChildren<LookAt3D>();
        }

        if (_turretProjectileLauncher == null)
        {
            _turretProjectileLauncher = GetComponent<ProjectileLauncher>();
        }

        _targeting.OnTargetChanged += TargetChanged;
    }


    private void OnDestroy()
    {
        _targeting.OnTargetChanged -= TargetChanged;
    }
    //Link the targets, it can be done by an event from the Targeting
    private void TargetChanged()
    {
        var target = _targeting.Target;
        if (_turrentLookAt2D != null)
        {
            _turrentLookAt2D.Target = target;
        }
        if (_turrentLookAt3D != null)
        {
            _turrentLookAt3D.Target = target;
        }
        _turretProjectileLauncher.Target = target;
    }

}
