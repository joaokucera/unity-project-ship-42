using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FriendItemSpawn : MonoBehaviour
{
    [SerializeField]
    private float spawnMinTime = 5f, spawnMaxTime = 10f;

    private float timeFactor = 0.5f;
    private SpawnStatus spawnStatus = SpawnStatus.INACTIVE;

    void Update()
    {
        if (spawnStatus == SpawnStatus.INACTIVE)
        {
            spawnStatus = SpawnStatus.ACTIVE;

            Invoke("Spawn", Random.Range(spawnMinTime, spawnMaxTime));
            Invoke("Inactive", Random.Range(spawnMaxTime - timeFactor, spawnMaxTime + timeFactor));
        }
    }

    private void Spawn()
    {
        if (transform.parent.gameObject.activeInHierarchy)
        {
            FriendItemPooling.Instance.SpawnFriendItemFromPool(transform.position);
        }

        CancelInvoke("Spawn");
    }

    private void Inactive()
    {
        spawnStatus = SpawnStatus.INACTIVE;

        CancelInvoke("Inactive");
    }
}
