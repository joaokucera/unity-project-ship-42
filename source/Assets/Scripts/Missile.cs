using UnityEngine;
using System.Collections;

public enum MissileAttack
{
    Straight,
    Curve
}

public class Missile : GenericMovement
{
    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public MissileAttack missileAttack;
    private Vector2 moveDirection;

    void FixedUpdate()
    {
        rigidbody2D.velocity = transform.up * verticalSpeed;
    }

    protected override void TranslateRightOrDown()
    {
        if (!target.renderer.isVisible)
        {
            missileAttack = MissileAttack.Straight;
        }

        switch (missileAttack)
        {
            case MissileAttack.Straight:
            default:
                rigidbody2D.gravityScale = -2;
                break;
            case MissileAttack.Curve:
                Vector3 normalized = (target.position - transform.position).normalized;
                float angle = Mathf.Atan2(normalized.y, normalized.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
                break;
        }
    }
}