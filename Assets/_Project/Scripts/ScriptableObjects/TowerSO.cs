using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TowerScriptableObject", order = 1)]
public class TowerSO : ScriptableObject
{
    public string NameId;
    public Image Icon;
    public int Cost;

    public GameObject Prefab;
}
