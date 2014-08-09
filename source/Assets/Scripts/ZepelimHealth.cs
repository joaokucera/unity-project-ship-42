using UnityEngine;
using System.Collections;

public class ZepelimHealth : EnemyHealth
{
    [SerializeField]
    private float respawnTime = 10f;

    void Update()
    {
        if (health <= 0)
        {
            SendMessage("IncreaseSpeed");
        }
    }

    void OnBecameInvisible()
    {
        Invoke("Respawn", respawnTime);
    }

    private void Respawn()
    {
        startHealth++;

        enemyScript.TurnBack();
    }
}
