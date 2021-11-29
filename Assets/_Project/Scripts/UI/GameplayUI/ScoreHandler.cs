using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private int _score = 0;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        EventManager.Listen(eEvent.LoadScene, ResetScore);
        ResetScore();
    }

    private void OnDestroy()
    {
        EventManager.Remove(eEvent.LoadScene, ResetScore);
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        EventManager.Listen(eEvent.PointsScored, OnPointsScored);
    }
    
    private void OnDisable()
    {
        EventManager.Remove(eEvent.PointsScored, OnPointsScored);
    }

    private void ResetScore()
    {
        _score = 0;
        _text.text = _score.ToString();
    }

    private void OnPointsScored(object[] args)
    {
        var points = (int)args[0];
        _score += points;
        _text.text = _score.ToString();
    }

}
