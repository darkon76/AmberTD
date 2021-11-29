using TMPro;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private HealthHolder _healthHolder;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        EventManager.Listen(eEvent.EnemyObjectCreated, OnEnemyObjectCreated);
        var eo = LevelManager.Instance.EnemyObjective;
        if (eo != null)
        {
            RegisterHealthHolder(eo);
        }
    }

    private void OnDestroy()
    {
        EventManager.Remove(eEvent.EnemyObjectCreated, OnEnemyObjectCreated);
        if (_healthHolder != null)
        {
            _healthHolder.OnHealthChanged -= HealthChanged;
            _healthHolder = null;
        }

    }

    private void RegisterHealthHolder(EnemyObjective eo)
    {
        if (_healthHolder != null)
        {
            _healthHolder.OnHealthChanged -= HealthChanged;
        }

        _healthHolder = eo.GetComponent<HealthHolder>();
        _healthHolder.OnHealthChanged += HealthChanged;
        HealthChanged();
    }

    private void OnEnemyObjectCreated(object[] arg)
    {
        var eo = arg[0] as EnemyObjective;
        RegisterHealthHolder(eo);
    }

    

    private void HealthChanged()
    {
        _text.text = _healthHolder.Current.ToString();
    }
}
