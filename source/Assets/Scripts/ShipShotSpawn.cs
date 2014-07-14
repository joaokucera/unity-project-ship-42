using UnityEngine;
using System.Collections;

public class ShipShotSpawn : MonoBehaviour
{
    [HideInInspector]
    public MissileAttack missileAttack = MissileAttack.Straight;

    protected Camera mainCamera;

    private const float StartTimerLevel = 5;
    private const int MissileComb = 4;

    [SerializeField]
    private LayerMask layerMask;
    private int missileCombAmmount;

    void Start()
    {
        mainCamera = Camera.main;
        missileCombAmmount = ShipShotSpawn.MissileComb;
    }

    void Update()
    {
#if UNITY_EDITOR
        MouseAction();
#else
		TouchAction ();
#endif
    }

    protected void MouseAction()
    {
        // Just 1 tap.
        if (Input.GetButtonDown("Fire1"))
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            CheckAction(mousePosition);
        }
    }

    void TouchAction()
    {
        if (Input.touchCount > 0 && missileCombAmmount >= 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);
                CheckAction(touchPosition);
            }
        }
    }

    private void CheckAction(Vector2 position)
    {
        ActivateMissile(position);
    }

    private void ActivateMissile(Vector2 position)
    {
        Collider2D collider = Physics2D.OverlapPoint(position, layerMask);

        if (collider.transform != null)
        {
            if (collider.transform.tag == "Enemy")
            {
                missileCombAmmount--;
                StartCoroutine(MissileCombCooldownVerification());

                ShipShotPooling.Instance.SpawnShotFromPool(transform.position, missileAttack, collider.transform);
            }
        }
    }

    private IEnumerator MissileCombCooldownVerification()
    {
        for (float timerLevel = StartTimerLevel; timerLevel >= 0; timerLevel -= Time.deltaTime)
        {
            yield return 0;
        }

        if (missileCombAmmount < MissileComb)
        {
            missileCombAmmount++;
        }
    }
}