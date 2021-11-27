using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControl : MonoBehaviour
{
    private LookAt2D _turrentLookAt2D;
    private Targeting _targeting;
    private Shooting _turretShooting;

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

        if (_turretShooting == null)
        {
            _turretShooting = GetComponent<Shooting>();
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
        _turretShooting.Target = target;
    }

}
