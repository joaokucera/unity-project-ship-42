using UnityEngine;
using System.Collections;

public class ShipShotPooling : GenericPooling {

    private static ShipShotPooling instance;
    public static ShipShotPooling Instance
    {
        get
        {
            if (ShipShotPooling.instance == null)
            {
                ShipShotPooling.instance = GameObject.Find("Generic Pooling").GetComponent<ShipShotPooling>();
            }

            return ShipShotPooling.instance;
        }
    }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        base.Initialize();
    }

    public void SpawnShotFromPool(Vector2 position, MissileAttack missileAttack, Transform target)
    {
        GameObject shot = GetObjectFromPool(position);

        if (shot != null)
        {
            shot.renderer.sortingLayerName = "Middleground";
            shot.renderer.sortingOrder = 0;

            Missile missile = shot.GetComponent<Missile>();
            missile.missileAttack = missileAttack;
            missile.target = target;

            SoundEffectScript.Instance.PlaySound(SoundEffectClip.ShipMissileAttack);
        }
    }
}
