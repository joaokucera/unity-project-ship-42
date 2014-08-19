using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudPooling : GenericPooling
{
    [SerializeField]
    private List<Sprite> cloudSprites;

    private static CloudPooling instance;
    public static CloudPooling Instance
    {
        get
        {
            if (CloudPooling.instance == null)
            {
                CloudPooling.instance = GameObject.Find("Generic Pooling").GetComponent<CloudPooling>();
            }

            return CloudPooling.instance;
        }
    }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        base.Initialize();

        if (cloudSprites == null || cloudSprites.Count == 0)
        {
            Debug.LogError("There are no cloud sprites available!");
        }
    }

    public void SpawnCloudFromPool(Vector2 position, CloudSize cloudSize)
    {
        GameObject cloud = GetObjectFromPool(position);

        if (cloud != null)
        {
            switch (cloudSize)
            {
                case CloudSize.Big:
                    cloud.transform.localScale = Vector3.one;
                    cloud.renderer.sortingLayerName = "Background";
                    cloud.renderer.sortingOrder = 3;
                    break;
                case CloudSize.Normal:
                    cloud.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                    cloud.renderer.sortingLayerName = "Background";
                    cloud.renderer.sortingOrder = 2;
                    break;
                case CloudSize.Little:
                    cloud.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    cloud.renderer.sortingLayerName = "Background";
                    cloud.renderer.sortingOrder = 1;
                    break;
            }

            int index = Random.Range(0, cloudSprites.Count);
            ((SpriteRenderer)cloud.renderer).sprite = cloudSprites[index];
        }
    }
}