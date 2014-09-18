using UnityEngine;
using System.Collections;

public class TargetPooling : GenericPooling
{
    private static TargetPooling instance;
    public static TargetPooling Instance
    {
        get
        {
            if (TargetPooling.instance == null)
            {
                TargetPooling.instance = GameObject.Find("Generic Pooling").GetComponent<TargetPooling>();
            }

            return TargetPooling.instance;
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

    public GameObject SpawnTargetFromPool(Vector2 position, Transform parent)
    {
        GameObject target = GetObjectFromPool(position);

        if (target != null)
        {
            target.transform.parent = parent;

            target.renderer.sortingLayerName = "Foreground";
            target.renderer.sortingOrder = 5;

            print("SPAWN TARGET (activeInHierarchy): " + target.activeInHierarchy);
        }

        return target;
    }
}
