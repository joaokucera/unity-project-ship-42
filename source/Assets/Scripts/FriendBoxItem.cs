using UnityEngine;
using System.Collections;

public enum FriendItem
{
    Coke,// = 1,
    Cookie,// = 2,
    Slice_Pizza,// = 3,
    Hamburguer,// = 4,
    Chicken,// = 6,
    Watermelon,// = 8,
    Whole_Pizza,// = 9, //3 x 3 (3 para cada personagem)
    Red_Cross,// = 10,
    NONE
}

public class FriendBoxItem : MonoBehaviour
{
    [HideInInspector]
    public FriendItem friendItem;
    private float speed = -2.5f;

    void FixedUpdate()
    {
        rigidbody2D.velocity = Vector2.up * speed;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && gameObject.activeInHierarchy)
        {
            SpawnParticleEffectsAndSound(transform.position);

            collider.gameObject.SendMessage("AddItem", friendItem);

            gameObject.SetActive(false);
        }
    }

    public void SetItem(int index)
    {
        friendItem = (FriendItem)index;
    }

    private void SpawnParticleEffectsAndSound(Vector2 position)
    {
        SoundEffectScript.Instance.PlaySound(SoundEffectClip.ShipTakeItemSound);

        FireworksPooling.Instance.SpawnFireworksFromPool(null, position);
    }
}
