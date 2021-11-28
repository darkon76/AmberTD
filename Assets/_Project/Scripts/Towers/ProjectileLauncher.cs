using System;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private DamageSourceSO _damageSource;
    [SerializeField]
    private float _shootCD = 1;

    [SerializeField] private Transform _muzzle;
    
    [Space]
    [SerializeField]
    private float _currentTime;
    [SerializeField]
    private GameObject _target;

    private bool _canShoot = true;
    public event Action OnShoot;

    public GameObject Target
    {
        get => _target;
        set
        {
            if (_target == value)
            {
                return;
            }

            _target = value;
        }
    }

    private void Update()
    {
        
        _currentTime += Time.deltaTime;

        if (_target == null || _currentTime < _shootCD || !_canShoot)
        {
            return;
        }

        _currentTime = 0;
        //Creates the projectile that will be shoot.
        _damageSource.CreateOne(_muzzle, Target.transform);
        OnShoot?.Invoke();
    }
}
