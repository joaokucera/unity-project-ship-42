using UnityEngine;
using System.Collections;

public class Barril : GenericMovement, IAmmo
{
    public int Damage
    {
        get { return (int)AmmoDamage.Barril; }
    }
}
