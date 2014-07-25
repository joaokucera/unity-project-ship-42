using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ShipShotSpawn : MonoBehaviour
{
    protected Camera mainCamera;

    private int missileAmmo;
    [SerializeField]
    private float cooldownMissileAmmo;
    [SerializeField]
    private int startMissileAmmo = 4;
    [SerializeField]
    private MissileAttack missileAttack;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private List<Renderer> ammoRenderers;

    void Start()
    {
        if (ammoRenderers == null || ammoRenderers.Count <= 0)
        {
            Debug.LogError("There are no ammo renderes available!");
        }

        mainCamera = Camera.main;
        missileAmmo = startMissileAmmo;
    }

    void Update()
    {
#if UNITY_EDITOR
        MouseAction();
#else
		TouchAction ();
#endif
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, Screen.height - 35, 200, 100), "AMMO: " + missileAmmo);
        GUI.Label(new Rect(10, Screen.height - 20, 200, 100), "COOLDOWN: " + cooldownMissileAmmo);
    }

    private void MouseAction()
    {
        // Just 1 tap.
        if (Input.GetButtonDown("Fire1"))
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            ActivateMissile(mousePosition);
        }
    }

    private void TouchAction()
    {
        if (Input.touchCount > 0 && missileAmmo >= 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);
                ActivateMissile(touchPosition);
            }
        }
    }

    private void ActivateMissile(Vector2 position)
    {
        Collider2D collider = Physics2D.OverlapPoint(position, layerMask);

        if (collider != null && collider.transform != null)
        {
            if (collider.transform.tag == "Enemy")
            {
                EnemyHealth enemy = collider.GetComponent<EnemyHealth>();
                //if (!enemy.marketAsTaget)
                {
                    enemy.marketAsTaget = true;

                    StartCoroutine(MissileAmmoCooldownVerification());

                    ShipShotPooling.Instance.SpawnShotFromPool(transform.position, missileAttack, collider.transform);
                }
            }
        }
    }

    private IEnumerator MissileAmmoCooldownVerification()
    {
        missileAmmo--;
        foreach (var item in ammoRenderers)
        {
            if (item.enabled)
            {
                item.enabled = false;
                break;
            }
        }

        cooldownMissileAmmo = 100 / CrewStatus.Instance.soldierStamina;
        for (float timerLevel = cooldownMissileAmmo; timerLevel >= 0; timerLevel -= Time.deltaTime)
        {
            yield return 0;
        }

        if (missileAmmo < startMissileAmmo)
        {
            missileAmmo++;
            foreach (var item in ammoRenderers)
            {
                if (!item.enabled)
                {
                    item.enabled = true;
                    break;
                }
            }
        }
    }
}