using System.Collections;
using UnityEngine;

public class ContinuousSpawner : BaseSpawner
{
    public float WarmCD = 0f;
    public float WaitBetweenSpawns = .25f;
    public float Radius;

    public bool Edge;

    //Quick and easy pairs.
    public GameObject Prefab;

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(WarmCD);
        var waitForSeconds = new WaitForSeconds(WaitBetweenSpawns);
        while (true)
        {
            GetPosition(Radius, Edge, out var position);
            var go = PoolManager.RequestGameObject(Prefab);
            go.transform.position = position;
            go.SetActive(true);
            yield return waitForSeconds;
        }
    }

    private void GetPosition(float radius, bool edge, out Vector3 position)
    {
        var randomPos = Random.insideUnitCircle;
        position = new Vector3(randomPos.x, 0, randomPos.y);
        if (edge)
        {
            position.Normalize();
        }

        position *= radius;
        position += transform.position;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
#if UNITY_EDITOR
        UnityEditor.Handles.color = Color.blue;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, Radius);
#endif
    }
}
