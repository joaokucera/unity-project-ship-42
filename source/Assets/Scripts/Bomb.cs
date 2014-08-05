using UnityEngine;
using System.Collections;

public class Bomb : GenericMovement, IAmmo, IDamage, IEnemy
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
    }

    void OnBecameVisible()
    {
        Splashed = false;
    }

    //void OnBecameInVisible()
    //{
    //    foreach (Transform chield in transform)
    //    {
    //        if (chield.tag == "Target")
    //        {
    //            chield.parent = null;
    //        }
    //    }
    //}

    public void SetDamage()
    {
        gameObject.SetActive(false);
    }
}