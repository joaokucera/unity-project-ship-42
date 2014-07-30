using UnityEngine;
using System.Collections;

public class Barril : GenericMovement, IAmmo
{
    public bool Splashed { get; set; }

    public int Damage
    {
        get { return (int)AmmoDamage.Barril; }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Wave" && !Splashed)
        {
            Splashed = true;

            WaterSplashPooling.Instance.SpawnSplashFromPool(transform.position);
        }

        if (collider.tag == "Missile" && collider.renderer.enabled)
        {
            collider.gameObject.SetActive(false);

            gameObject.SetActive(false);
        }
    }
}
