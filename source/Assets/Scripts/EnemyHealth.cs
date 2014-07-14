using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    public int health = 1;

    void Update()
    {
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Missile")
        {
            collider.gameObject.SetActive(false);

            health -= 1;
        }
    }
}
