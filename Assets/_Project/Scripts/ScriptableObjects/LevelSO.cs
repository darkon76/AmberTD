using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LevelSO")]
public class LevelSO : ScriptableObject
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;
}
