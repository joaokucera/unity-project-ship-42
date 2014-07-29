using UnityEngine;
using System.Collections;

public class Bomb : GenericMovement, IAmmo
{
    public int Damage
    {
        get { return (int)AmmoDamage.Bomb; }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Wave")
        {
            WaterSplashPooling.Instance.SpawnSplashFromPool(transform.position);
        }
    }
}