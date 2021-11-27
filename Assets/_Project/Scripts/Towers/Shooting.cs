using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private ProjectileControl _projectilePrefab;
    [SerializeField]
    private float _shootCD = 1;

    [SerializeField] private Transform _muzzle;
    
    [Space]
    [SerializeField]
    private float _currentTime;
    [SerializeField]
    private GameObject _target;

    private bool _canShoot = true;

    

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

        var projectileGO = PoolManager.RequestGameObject(_projectilePrefab.gameObject, 4);
        var projectile = projectileGO.GetComponent<ProjectileControl>();
        projectile.transform.SetPositionAndRotation(_muzzle.transform.position, _muzzle.transform.rotation);
        projectile.SetTarget(Target.transform);
        projectileGO.SetActive(true);

    }

}
