using UnityEngine;
using System.Collections;

public class EnemyShotPooling : GenericPooling
{
    private static EnemyShotPooling instance;
    public static EnemyShotPooling Instance
    {
        get
        {
            if (EnemyShotPooling.instance == null)
            {
                EnemyShotPooling.instance = GameObject.Find("Generic Pooling").GetComponent<EnemyShotPooling>();
            }

            return EnemyShotPooling.instance;
        }
    }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        base.Initialize();
    }

    public void SpawnShotFromPool(Vector2 position)
    {
        GameObject shot = GetObjectFromPool(position);

        if (shot != null)
        {
            shot.renderer.sortingLayerName = "Foreground";
            shot.renderer.sortingOrder = 0;

            SoundEffectScript.Instance.PlaySound(SoundEffectClip.EnemyBombAttack);
        }
    }
}
