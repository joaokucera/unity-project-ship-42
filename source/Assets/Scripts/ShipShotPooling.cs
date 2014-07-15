using UnityEngine;
using System.Collections;

public class ShipShotPooling : GenericPooling {

    public static ShipShotPooling Instance;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        base.Initialize();
    }

    public void SpawnShotFromPool(Vector2 position, MissileAttack missileAttack, Transform target)
    {
        GameObject shot = GetObjectFromPool(position);

        if (shot != null)
        {
            shot.renderer.sortingLayerName = "Foreground";
            shot.renderer.sortingOrder = 0;

            Missile missile = shot.GetComponent<Missile>();
            missile.missileAttack = missileAttack;
            missile.target = target;
        }
    }
}
