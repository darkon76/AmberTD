using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControl : MonoBehaviour
{
    private LookAt2D _turrentLookAt2D;
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

    private void TargetChanged()
    {
        var target = _targeting.Target;
        _turrentLookAt2D.Target = target;
        _turretProjectileLauncher.Target = target;
    }

}
