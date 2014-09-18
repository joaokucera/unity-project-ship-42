using UnityEngine;
using System.Collections;

public class WaterSplashPooling : GenericPooling
{
    private static WaterSplashPooling instance;
    public static WaterSplashPooling Instance
    {
        get
        {
            if (WaterSplashPooling.instance == null)
            {
                WaterSplashPooling.instance = GameObject.Find("Generic Pooling").GetComponent<WaterSplashPooling>();
            }

            return WaterSplashPooling.instance;
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

    public void SpawnSplashFromPool(Vector2 position)
    {
        GameObject splash = GetObjectFromPool(position);

        if (splash != null)
        {
            SpriteRenderer splashRenderer = splash.GetComponentInChildren<SpriteRenderer>();
 
            splashRenderer.sortingLayerName = "Foreground";
            splashRenderer.sortingOrder = 0;

            SoundEffectScript.Instance.PlaySound(SoundEffectClip.EnemyBombFallingOcean);
        }
    }
}
