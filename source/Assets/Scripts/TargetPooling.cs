using UnityEngine;
using System.Collections;

public class TargetPooling : GenericPooling {

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

    public void SpawnTargetFromPool(Transform parent)
    {
        GameObject shot = GetObjectFromPool(parent.position);

        if (shot != null)
        {
            shot.transform.parent = parent;

            shot.renderer.sortingLayerName = "Foreground";
            shot.renderer.sortingOrder = 2;
        }
    }
}
