﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum EnemyType
{
    Zepelim,
    BadassAirplane
}

public class EnemyHealth : MonoBehaviour, IDamage
{
    [SerializeField]
    protected int startHealth = 1;
    protected int health;
    protected Enemy enemyScript;

    [SerializeField]
    private EnemyType enemyType = EnemyType.BadassAirplane;

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        health = startHealth;

        enemyScript = GetComponent<Enemy>();
    }

    void OnBecameVisible()
    {
        health = startHealth;
    }

    public void SetDamage()
    {
        health -= 1;

        DeactivateTargets();

        if (enemyType == EnemyType.Zepelim)
        {
            SendMessage("IncreaseTorque");
        }
        else if (enemyType == EnemyType.BadassAirplane && health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void DeactivateTargets()
    {
        Target target = GetComponentsInChildren<Target>().FirstOrDefault();
        if (target != null)
        {
            target.Deactivate();
        }
    }
}
