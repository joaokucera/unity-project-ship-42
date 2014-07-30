using UnityEngine;
using System.Collections;

public class Bomb : GenericMovement, IAmmo
{
    public bool Splashed { get; set; }

    void Start()
    {
        base.Initialize();

        TrailRenderer trail = GetComponentInChildren<TrailRenderer>();

        if (trail != null)
        {
            trail.sortingLayerName = "Foreground";
            trail.sortingOrder = 0;
        }
    }

    public int Damage
    {
        get { return (int)AmmoDamage.Bomb; }
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

    void OnBecameVisible()
    {
        Splashed = false;
    }
}