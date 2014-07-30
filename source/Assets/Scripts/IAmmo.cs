using UnityEngine;
using System.Collections;

public enum AmmoDamage
{
    Missile = 1,
    Barril = 10,
    Bomb = 20
}

public interface IAmmo
{
    bool Splashed { get; set; }

    int Damage { get; }
}
