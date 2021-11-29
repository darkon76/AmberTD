using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TowerScriptableObject", order = 1)]
public class TowerSO : ScriptableObject
{
    public string NameId;
    public Sprite Icon;
    public int Cost;
    
    public float AttackRadius = 1;
    public float AttackCd = 1f;
    public GameObject Prefab;
    public DamageSourceSO Projectile;


    /// <summary>
    /// Creates the gameobject tower and initialize it. 
    /// </summary>
    /// <param name="position"></param>
    public void Create(Vector3 position)
    {
        var go = PoolManager.RequestGameObject(Prefab);
        var targeting = go.GetComponent<Targeting>();
        targeting.SetRadius(AttackRadius);
        var launcher = go.GetComponent<ProjectileLauncher>();
        launcher.DamageSource = Projectile;
        launcher.CD = AttackCd;
        go.transform.position = position;
        go.SetActive(true);
        AstarPath.active.Scan();
    }
}
