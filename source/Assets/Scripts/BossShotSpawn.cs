using UnityEngine;
using System.Collections;

public class BossShotSpawn : MonoBehaviour
{

    [SerializeField]
    private LayerMask layerMask;
    private ShotStatus shotStatus = ShotStatus.INACTIVE;

    void Update()
    {
        if (shotStatus == ShotStatus.INACTIVE)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, float.MaxValue, layerMask);

            if (hit.transform != null)
            {
                if (hit.transform.tag == "Player")
                {
                    shotStatus = ShotStatus.ACTIVE;

                    Invoke("Shoot", Random.Range(0f, 1f));
                    Invoke("Inactive", Random.Range(0.5f, 1.5f));
                }
            }
        }
    }

    private void Shoot()
    {
        if (transform.parent.gameObject.activeInHierarchy)
        {
            BossShotPooling.Instance.SpawnShotFromPool(transform.position);
        }

        CancelInvoke("Shoot");
    }

    private void Inactive()
    {
        shotStatus = ShotStatus.INACTIVE;

        CancelInvoke("Inactive");
    }
}
