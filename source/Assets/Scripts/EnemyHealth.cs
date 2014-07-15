using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int startHealth = 1;
    private int health;

    void Start()
    {
        health = startHealth;
    }

    void Update()
    {
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Missile" && collider.renderer.enabled)
        {
            collider.gameObject.SetActive(false);

            health -= 1;
        }
    }

    void OnBecameVisible()
    {
        health = startHealth;
    }
}
