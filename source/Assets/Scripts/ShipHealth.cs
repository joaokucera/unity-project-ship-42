using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipHealth : MonoBehaviour
{
    private const int StartHealth = 100;

    private float cooldownHealth;
    private int health;
    private float loadBarFactor;

    [SerializeField]
    private GUITexture shipBox = null;
    [SerializeField]
    private GUITexture shipLoadBar = null;

    private bool isDead = false;

    void Start()
    {
        GameSettings.Instance.sailedTime = 0;

        health = StartHealth;
        loadBarFactor = shipBox.pixelInset.width / StartHealth;

        // HUD Ship Box & Load Bar.
        shipBox.transform.position = new Vector2(0.5f, 0.03f);
        shipLoadBar.transform.position = new Vector2(0.5f, 0.03f);
    }

    void Update()
    {
        if (health <= 0 && !isDead)
        {
            isDead = true;

            transform.GetChild(0).gameObject.SetActive(false);

            gameObject.SendMessage("Die");
        }
        else
        {
            GameSettings.Instance.sailedTime += Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "EnemyAmmo" && collider.renderer.enabled)
        {
            Vector2 position = collider.transform.position - Vector3.up * 1.5f;
            SpawnParticleEffectsAndSound(position);

            collider.gameObject.SetActive(false);
            StartCoroutine(HealthCooldownVerification(((IAmmo)collider.GetComponent<GenericMovement>()).Damage));

            if (GameSettings.Instance.hapticsEnabled)
            {
                Handheld.Vibrate();
            }
        }
    }

    private void SpawnParticleEffectsAndSound(Vector2 position)
    {
        SoundEffectScript.Instance.PlaySound(SoundEffectClip.ShipHit);

        ExplosionPooling.Instance.SpawnExplosionFromPool(transform, position);
        SmokePooling.Instance.SpawnSmokeFromPool(transform, position);
    }

    private void SetHealthBar()
    {
        float value = loadBarFactor * health;

        float clamp = Mathf.Clamp(value, 0, shipBox.pixelInset.width);

        shipLoadBar.pixelInset = new Rect(shipLoadBar.pixelInset.x, shipLoadBar.pixelInset.y, clamp, shipLoadBar.pixelInset.height);
    }

    private IEnumerator HealthCooldownVerification(int damage)
    {
        health -= damage;
        SetHealthBar();

        while (health < StartHealth)
        {
            float adder = Time.deltaTime;
            cooldownHealth = 500 / CrewStatus.Instance.mechanicStamina;

            for (float timer = 0; timer <= cooldownHealth; timer += adder)
            {
                CrewStatus.Instance.LoadBarMechanic(cooldownHealth, adder);

                yield return 0;
            }

            if (!isDead && health < StartHealth)
            {
                health++;
                SetHealthBar();

                CrewStatus.Instance.ClearBarMechanic();
            }

            yield return 0;
        }
    }
}