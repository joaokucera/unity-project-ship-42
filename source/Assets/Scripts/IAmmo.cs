using UnityEngine;
using System.Collections;

public enum AmmoDamage
{
    Barril = 10,
    Bomb = 20
}

public interface IAmmo
{
    int Damage
    {
        get;
    }
}
