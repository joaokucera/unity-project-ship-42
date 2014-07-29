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

        Vector2 newPosition = splash.transform.position;
        newPosition.y -= splash.renderer.bounds.size.y;
        splash.transform.position = newPosition;

        if (splash != null)
        {
            splash.renderer.sortingLayerName = "Foreground";
            splash.renderer.sortingOrder = 0;
        }
    }
}
