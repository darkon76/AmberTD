using System;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] public DamageSourceSO DamageSource;

    [SerializeField] private Transform _muzzle;

    public float CD;
    
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

        if (_target == null || _currentTime < CD || !_canShoot)
        {
            return;
        }

        _currentTime = 0;
        //Creates the projectile that will be shoot.
        try
        {
            DamageSource.CreateOne(gameObject, Target, _muzzle);

        }
        catch (Exception e)
        {
            int a = 0;
            a++;
        }
        OnShoot?.Invoke();
    }
}
