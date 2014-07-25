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

    public void SpawnCloudFromPool(Vector2 position)
    {
        GameObject cloud = GetObjectFromPool(position);

        if (cloud != null)
        {
            cloud.renderer.sortingLayerName = "Background";
            cloud.renderer.sortingOrder = 1;

            int index = Random.Range(0, CloudPooling.instance.cloudSprites.Count);
            ((SpriteRenderer)cloud.renderer).sprite = CloudPooling.instance.cloudSprites[index];
        }
    }
}