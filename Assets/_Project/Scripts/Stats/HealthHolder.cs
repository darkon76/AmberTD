using System;
using UnityEngine;

public class HealthHolder : MonoBehaviour
{
    public float Max = 10;
    [SerializeField] private float _current = 10;

    public event Action OnDead;
    public event Action OnHealthChanged;
    public bool IsAlive = true;

    public float Current
    {
        get => _current;
        set
        {
            value = Mathf.Clamp(value, 0, Max);
            if (_current == value)
            {
                return;
            }

            _current = value;

            if (_current <= 0 && IsAlive)
            {
                IsAlive = false;
                OnDead?.Invoke();
            }

            if (_current > 0)
            {
                IsAlive = true;
            }
            OnHealthChanged?.Invoke();
        }
    }
}
