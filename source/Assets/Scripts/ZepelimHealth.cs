using UnityEngine;
using System.Collections;

public class ZepelimHealth : EnemyHealth
{
    /// <summary>
    /// BALANCE: Spawn a cada 21 segundos.
    /// </summary>
    private float respawnTime = 21f;

    void Update()
    {
        if (health <= 0)
        {
            SendMessage("IncreaseSpeed");
        }
    }

    void OnBecameVisible()
    {
        SoundEffectScript.Instance.PlaySound(SoundEffectClip.EnemyShowingSound);

        health = startHealth;
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
