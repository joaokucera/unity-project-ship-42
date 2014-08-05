using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ShipShotSpawn : MonoBehaviour
{
    protected Camera mainCamera;

    private const int StartMissileAmmo = 4;

    [SerializeField]
    private MissileAttack missileAttack;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private List<Renderer> ammoRenderers;

    private int missileAmmo;
    private float cooldownMissileAmmo;

    void Start()
    {
        if (ammoRenderers == null || ammoRenderers.Count <= 0)
        {
            Debug.LogError("There are no ammo renderes available!");
        }

        mainCamera = Camera.main;
        missileAmmo = StartMissileAmmo;
    }

    void Update()
    {
#if UNITY_EDITOR
        MouseAction();
#else
		TouchAction ();
#endif
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
        if (Input.touchCount > 0)
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

        if (collider != null && collider.transform != null && missileAmmo > 0)
        {
            if (collider.transform.tag.Contains("Enemy"))
            {
                TargetPooling.Instance.SpawnTargetFromPool(collider.transform);

                StartCoroutine(MissileAmmoCooldownVerification());

                ShipShotPooling.Instance.SpawnShotFromPool(transform.position, missileAttack, collider.transform);
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

        float adder = Time.deltaTime;
        cooldownMissileAmmo = 100 / CrewStatus.Instance.soldierStamina;

        for (float timer = 0; timer <= cooldownMissileAmmo; timer += adder)
        {
            CrewStatus.Instance.LoadBarSoldier(cooldownMissileAmmo, adder);

            yield return 0;
        }

        if (missileAmmo < StartMissileAmmo)
        {
            missileAmmo++;
            foreach (var item in ammoRenderers)
            {
                if (!item.enabled)
                {
                    item.enabled = true;
                    CrewStatus.Instance.ClearBarSoldier();

                    break;
                }
            }
        }
    }
}