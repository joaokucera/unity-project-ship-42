using UnityEngine;
using System.Collections;

public class EnemyShotSpawn : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float spawnMinTime = 0f, spawnMaxTime = 1f;

    private float timeFactor = 0.5f;
    private SpawnStatus spawnStatus = SpawnStatus.INACTIVE;

    void Update()
    {
        if (spawnStatus == SpawnStatus.INACTIVE)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, float.MaxValue, layerMask);

            if (hit.transform != null)
            {
                if (hit.transform.tag == "Player")
                {
                    spawnStatus = SpawnStatus.ACTIVE;

                    Invoke("Spawn", Random.Range(spawnMinTime, spawnMaxTime));
                    Invoke("Inactive", Random.Range(spawnMaxTime - timeFactor, spawnMaxTime + timeFactor));
                }
            }
        }
    }

    private void Spawn()
    {
        if (transform.parent.gameObject.activeInHierarchy)
        {
            EnemyShotPooling.Instance.SpawnShotFromPool(transform.position);
        }

        CancelInvoke("Spawn");
    }

    private void Inactive()
    {
        spawnStatus = SpawnStatus.INACTIVE;

        CancelInvoke("Inactive");
    }
}