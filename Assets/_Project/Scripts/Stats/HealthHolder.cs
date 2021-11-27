using System;
using UnityEngine;

public class HealthHolder : MonoBehaviour
{
    [SerializeField] private float Max = 10;
    [SerializeField] private float _current = 10;

    public event Action OnDead;
    public event Action OnHealthChanged;

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
            OnHealthChanged?.Invoke();

            if (_current <= 0)
            {
                OnDead?.Invoke();
            }
        }
    }
}
