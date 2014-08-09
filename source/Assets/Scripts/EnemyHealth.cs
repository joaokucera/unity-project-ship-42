using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemyHealth : MonoBehaviour, IDamage
{
    [SerializeField]
    protected int startHealth = 1;
    protected int health;
    protected Enemy enemyScript;

    void Start()
    {
        health = startHealth;

        enemyScript = GetComponent<Enemy>();
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
        DeactivateTargets();

        if (health > 0)
        {
            SendMessage("IncreaseTorque");
        }
    }

    public void DeactivateTargets()
    {
        Target target = GetComponentsInChildren<Target>().FirstOrDefault();
        if (target != null)
        {
            target.SendMessage("Deactivate");
        }
    }
}
