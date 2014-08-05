using UnityEngine;
using System.Collections;

public class Barril : GenericMovement, IAmmo, IDamage, IEnemy
{
    [SerializeField]
    private float turningSpeed = 20f;

    public bool Splashed { get; set; }
    public int Damage
    {
        get { return (int)AmmoDamage.Barril; }
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, turningSpeed * Time.deltaTime));

        base.Updating();
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

        transform.rotation = Quaternion.Euler(Vector3.zero);
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
