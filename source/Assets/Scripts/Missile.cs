using UnityEngine;
using System.Collections;

public enum MissileAttack
{
    Straight,
    Curve
}

public class Missile : GenericMovement
{
    [HideInInspector] public Transform target;
    [HideInInspector] public MissileAttack missileAttack;

    protected override void TranslateRightOrDown()
    {
        switch (missileAttack)
        {
            case MissileAttack.Straight:
            default:
                transform.TranslateTo(horizontalSpeed, verticalSpeed, 0, Time.deltaTime);
                break;
            case MissileAttack.Curve:
                transform.position = Vector3.Slerp(transform.position, target.position, verticalSpeed);
                break;
        }
    }
}