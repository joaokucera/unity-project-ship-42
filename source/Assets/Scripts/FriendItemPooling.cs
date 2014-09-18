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

    /// <summary>
    /// BALANCE: De 1 a 8 itens, aumentando a cada 21 segundos.
    /// </summary>
    private int limitIndex;
    private const int minLimitIndex = 1;
    private int maxLimitIndex;
    private const float timeToImprove = 21;

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

        limitIndex = minLimitIndex;
        maxLimitIndex = friendItemSprites.Count;

        StartCoroutine(Improve());
    }

    private IEnumerator Improve()
    {
        if (GameSettings.Instance.SailedTime % timeToImprove == 0)
        {
            limitIndex++;
            limitIndex = Mathf.Clamp(limitIndex, minLimitIndex, maxLimitIndex);

            print("ITEM: " + limitIndex);
        }

        yield return 0;
    }

    public void SpawnFriendItemFromPool(Vector2 position)
    {
        GameObject friendItem = GetObjectFromPool(position);

        if (friendItem != null)
        {
            friendItem.renderer.sortingLayerName = "Foreground";
            friendItem.renderer.sortingOrder = 0;

            int index = Random.Range(0, limitIndex);
            ((SpriteRenderer)friendItem.renderer).sprite = friendItemSprites[index];

            friendItem.SendMessage("SetItem", index);
        }
    }
}
