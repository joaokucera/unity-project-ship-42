using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FriendItemPooling : GenericPooling
{
    [SerializeField]
    private List<Sprite> friendItemSprites;

    private static FriendItemPooling instance;
    public static FriendItemPooling Instance
    {
        get
        {
            if (FriendItemPooling.instance == null)
            {
                FriendItemPooling.instance = GameObject.Find("Generic Pooling").GetComponent<FriendItemPooling>();
            }

            return FriendItemPooling.instance;
        }
    }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        base.Initialize();

        if (friendItemSprites == null || friendItemSprites.Count == 0)
        {
            Debug.LogError("There are no friend item sprites available!");
        }
    }

    public void SpawnFriendItemFromPool(Vector2 position)
    {
        GameObject friendItem = GetObjectFromPool(position);

        if (friendItem != null)
        {
            friendItem.renderer.sortingLayerName = "Foreground";
            friendItem.renderer.sortingOrder = 0;

            int index = Random.Range(0, friendItemSprites.Count);
            ((SpriteRenderer)friendItem.renderer).sprite = friendItemSprites[index];

            friendItem.SendMessage("SetItem", index);
        }
    }
}
