using UnityEngine;
using System.Collections;

public class SmokePooling : GenericPooling
{
    private static SmokePooling instance;
    public static SmokePooling Instance
    {
        get
        {
            if (SmokePooling.instance == null)
            {
                SmokePooling.instance = GameObject.Find("Generic Pooling").GetComponent<SmokePooling>();
            }

            return SmokePooling.instance;
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

    public void SpawnSmokeFromPool(Transform parent, Vector2 position)
    {
        GameObject smoke = GetObjectFromPool(position);

        if (smoke != null && smoke.particleSystem != null)
        {
                smoke.SendMessage("InvokeDeactivate");
                smoke.transform.parent = parent;

                smoke.particleSystem.renderer.sortingLayerName = "Foreground";
                smoke.particleSystem.renderer.sortingOrder = 2;
        }
    }
}
