using UnityEngine;
using System.Collections;

public enum FriendItem
{
    RedCross,// = 10,
    WholePizza,// = 9, //3 x 3 (3 para cada personagem)
    WaterMellon,// = 8,
    Chicken,// = 6,
    Hamburguer,// = 4,
    SlicePizza,// = 3,
    Cookie,// = 2,
    Coke// = 1
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
            collider.gameObject.SendMessage("AddItem", friendItem);

            gameObject.SetActive(false);
        }
    }

    public void SetItem(int index)
    {
        friendItem = (FriendItem)index;
    }
}
