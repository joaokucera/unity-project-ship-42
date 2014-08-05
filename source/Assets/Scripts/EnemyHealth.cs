using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour, IDamage
{
    [SerializeField]
    private int startHealth = 1;
    protected int health;

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

    void OnBecameVisible()
    {
        health = startHealth;
    }

    public void SetDamage()
    {
        health -= 1;

        if (health > 0)
        {
            SendMessage("IncreaseTorque");
        }
    }
}
