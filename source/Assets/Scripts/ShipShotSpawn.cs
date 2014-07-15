using UnityEngine;
using System.Collections;

public class ShipShotSpawn : MonoBehaviour
{
    protected Camera mainCamera;

    private int missileAmmo;
    [SerializeField]
    private float startTimerLevel = 5;
    [SerializeField]
    private int startMissileAmmo = 4;
    [SerializeField]
    private MissileAttack missileAttack;
    [SerializeField]
    private LayerMask layerMask;

    void Start()
    {
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
        GUI.Label(new Rect(10, Screen.height - 20, 200, 100), "AMMO: " + missileAmmo);
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
                missileAmmo--;
                //StartCoroutine(MissileCombCooldownVerification());

                ShipShotPooling.Instance.SpawnShotFromPool(transform.position, missileAttack, collider.transform);
            }
        }
    }

    private IEnumerator MissileCombCooldownVerification()
    {
        for (float timerLevel = startTimerLevel; timerLevel >= 0; timerLevel -= Time.deltaTime)
        {
            yield return 0;
        }

        if (missileAmmo < startMissileAmmo)
        {
            missileAmmo++;
        }
    }
}