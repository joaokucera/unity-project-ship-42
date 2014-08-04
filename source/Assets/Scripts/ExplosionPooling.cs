using UnityEngine;
using System.Collections;

public class ExplosionPooling : GenericPooling
{
    private static ExplosionPooling instance;
    public static ExplosionPooling Instance
    {
        get
        {
            if (ExplosionPooling.instance == null)
            {
                ExplosionPooling.instance = GameObject.Find("Generic Pooling").GetComponent<ExplosionPooling>();
            }

            return ExplosionPooling.instance;
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

    public void SpawnExplosionFromPool(Transform parent, Vector2 position)
    {
        GameObject explosion = GetObjectFromPool(position - Vector2.up * 1.5f);

        if (explosion != null && explosion.particleSystem != null)
        {
            explosion.SendMessage("InvokeDeactivate");
            explosion.transform.parent = parent;

            explosion.particleSystem.renderer.sortingLayerName = "Foreground";
            explosion.particleSystem.renderer.sortingOrder = 2;
        }
    }
}
