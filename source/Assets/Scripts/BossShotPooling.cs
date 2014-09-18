using UnityEngine;
using System.Collections;

public class BossShotPooling : GenericPooling
{
    private static BossShotPooling instance;
    public static BossShotPooling Instance
    {
        get
        {
            if (BossShotPooling.instance == null)
            {
                BossShotPooling.instance = GameObject.Find("Generic Pooling").GetComponent<BossShotPooling>();
            }

            return BossShotPooling.instance;
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
            shot.GetComponentInChildren<Renderer>().sortingLayerName = "Foreground";
            shot.GetComponentInChildren<Renderer>().sortingOrder = 0;

            SoundEffectScript.Instance.PlaySound(SoundEffectClip.EnemyBarrilAttack);
        }
    }
}
