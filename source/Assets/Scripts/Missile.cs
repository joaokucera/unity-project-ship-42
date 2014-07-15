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
                transform.TranslateTo(horizontalSpeed, verticalSpeed * 3, 0, Time.deltaTime);
                break;
            case MissileAttack.Curve:
                transform.position = Vector2.Lerp(transform.position, target.position, verticalSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, verticalSpeed * Time.deltaTime);
                break;
        }
    }
}