﻿using UnityEngine;
using System.Collections;

public enum MissileAttack
{
    Straight,
    Curve
}

public class Missile : GenericMovement, IAmmo
{
    public bool Splashed { get; set; }

    public int Damage
    {
        get { return (int)AmmoDamage.Missile; }
    }

    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public MissileAttack missileAttack;
    private Vector2 moveDirection;

    void Start()
    {
        base.Initialize();

        TrailRenderer trail = GetComponentInChildren<TrailRenderer>();

        if (trail != null)
        {
            trail.sortingLayerName = "Middleground";
            trail.sortingOrder = 0;
        }
    }

    void FixedUpdate()
    {
        rigidbody2D.velocity = transform.up * verticalSpeed;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Contains("Enemy") && collider.renderer.enabled)
        {
            SpawnParticleEffectsAndSound(transform.position, collider.name);

            collider.SendMessage("SetDamage");

            gameObject.SetActive(false);

            if (GameSettings.Instance.hapticsEnabled)
            {
                Handheld.Vibrate();
            }
        }
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

    private void SpawnParticleEffectsAndSound(Vector2 position, string name)
    {
        if (name.Contains("Zepelim"))
        {
            SoundEffectScript.Instance.PlaySound(SoundEffectClip.EnemyHit);
        }
        else
        {
            SoundEffectScript.Instance.PlaySound(SoundEffectClip.EnemyDestroyed);
        }

        ExplosionPooling.Instance.SpawnExplosionFromPool(null, position);
        SmokePooling.Instance.SpawnSmokeFromPool(null, position);
    }
}