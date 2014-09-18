using UnityEngine;
using System.Collections;

public class FireworksPooling : GenericPooling
{
    private static FireworksPooling instance;
    public static FireworksPooling Instance
    {
        get
        {
            if (FireworksPooling.instance == null)
            {
                FireworksPooling.instance = GameObject.Find("Generic Pooling").GetComponent<FireworksPooling>();
            }

            return FireworksPooling.instance;
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

    public void SpawnFireworksFromPool(Transform parent, Vector2 position)
    {
        GameObject fireworks = GetObjectFromPool(position - Vector2.up);

        if (fireworks != null && fireworks.particleEmitter != null)
        {
            fireworks.SendMessage("InvokeDeactivate");
            fireworks.transform.parent = parent;

            fireworks.particleEmitter.renderer.sortingLayerName = "Foreground";
            fireworks.particleEmitter.renderer.sortingOrder = 2;
        }
    }
}
