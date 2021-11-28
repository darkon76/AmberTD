using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulkSpawner : MonoBehaviour
{

    public float WaitBetweenSpawns = .25f;
    public float Radius;
    public bool Edge;
    //Quick and easy pairs.
    public List<GameObject> Prefab;

    public List<int> Count;

    IEnumerator SpawnRoutine()
    {
        var waitForSeconds = new WaitForSeconds(WaitBetweenSpawns);
        for (int i = 0; i < Prefab.Count; i++)
        {
            //Position
            var count = Count[i];
            for (int j = 0; j < count; j++)
            {
                GetPosition(Radius, Edge, out var position);
                var go = PoolManager.RequestGameObject(Prefab[i]);
                go.transform.position = position;
                go.SetActive(true);
                yield return waitForSeconds;
            }
        }
    }


    [ContextMenu("Spawn")]
    public void Spawn()
    {
       StopAllCoroutines();
       StartCoroutine(SpawnRoutine());
    }

    private void GetPosition(float radius, bool edge, out Vector3 position)
    {
        var randomPos = Random.insideUnitCircle;
        position = new Vector3( randomPos.x, 0, randomPos.y);
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
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
