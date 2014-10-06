using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrewStatus : MonoBehaviour
{
    private static CrewStatus instance;
    public static CrewStatus Instance
    {
        get
        {
            if (CrewStatus.instance == null)
            {
                print(GameObject.Find("Crew Status").name);

                CrewStatus.instance = GameObject.Find("Crew Status").GetComponent<CrewStatus>();
            }

            return CrewStatus.instance;
        }
    }

    public bool CaptainBoost
    {
        set
        {
            captainBoostText.enabled = value;
        }
    }

    private Camera mainCamera;
    public float captainStamina = 50, mechanicStamina = 50, soldierStamina = 50;
    public GUIText captainText = null, mechanicText = null, soldierText = null;

    [SerializeField]
    private float crewStaminaDecreaseFactor = 1, captainBoostDecreaseFactor = 20;
    [SerializeField]
    private GUITexture captainTexture = null, mechanicTexture = null, soldierTexture = null;
    [SerializeField]
    private GUITexture captainBox = null, mechanicBox = null, soldierBox = null;
    [SerializeField]
    private GUITexture captainLoadBar = null, mechanicLoadBar = null, soldierLoadBar = null;
    [SerializeField]
    private GUIText captainBoostText = null;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        ClearBarCaptain();
        ClearBarMechanic();
        ClearBarSoldier();

        mainCamera = Camera.main;
        Vector2 viewport = mainCamera.WorldToViewportPoint(new Vector2(-mainCamera.aspect * mainCamera.orthographicSize, mainCamera.orthographicSize));

        // HUD Crew Pictures.
        captainTexture.transform.position = new Vector2(viewport.x + 0.05f, viewport.y - 0.1f);
        mechanicTexture.transform.position = new Vector2(viewport.x + 0.15f, viewport.y - 0.1f);
        soldierTexture.transform.position = new Vector2(viewport.x + 0.25f, viewport.y - 0.1f);

        // HUD Crew Health.
        captainText.transform.position = new Vector2(viewport.x + 0.05f, viewport.y - 0.18f);
        mechanicText.transform.position = new Vector2(viewport.x + 0.15f, viewport.y - 0.18f);
        soldierText.transform.position = new Vector2(viewport.x + 0.25f, viewport.y - 0.18f);

        // HUD Crew Box.
        captainBox.transform.position = new Vector2(viewport.x + 0.05f, viewport.y - 0.03f);
        mechanicBox.transform.position = new Vector2(viewport.x + 0.15f, viewport.y - 0.03f);
        soldierBox.transform.position = new Vector2(viewport.x + 0.25f, viewport.y - 0.03f);

        // HUD Crew Load Bar.
        captainLoadBar.transform.position = new Vector2(viewport.x + 0.05f, viewport.y - 0.03f);
        mechanicLoadBar.transform.position = new Vector2(viewport.x + 0.15f, viewport.y - 0.03f);
        soldierLoadBar.transform.position = new Vector2(viewport.x + 0.25f, viewport.y - 0.03f);

        // HUD Captain Boost.
        captainBoostText.transform.position = new Vector2(viewport.x + 0.05f, viewport.y - 0.21f);
        captainBoostText.enabled = false;
    }

    void Update()
    {
        SetCrewStamina();

        captainText.text = string.Format("{0}/100", (int)captainStamina);
        mechanicText.text = string.Format("{0}/100", (int)mechanicStamina);
        soldierText.text = string.Format("{0}/100", (int)soldierStamina);
    }

    private void SetCrewStamina()
    {
        float value = Time.deltaTime / crewStaminaDecreaseFactor;

        captainStamina -= value;
        mechanicStamina -= value;
        soldierStamina -= value;
    }

    public void ClearBarCaptain()
    {
        captainLoadBar.pixelInset = new Rect(captainLoadBar.pixelInset.x, captainLoadBar.pixelInset.y, 0, captainLoadBar.pixelInset.height);
    }

    public void ClearBarMechanic()
    {
        mechanicLoadBar.pixelInset = new Rect(mechanicLoadBar.pixelInset.x, mechanicLoadBar.pixelInset.y, 0, mechanicLoadBar.pixelInset.height);
    }

    public void ClearBarSoldier()
    {
        soldierLoadBar.pixelInset = new Rect(soldierLoadBar.pixelInset.x, soldierLoadBar.pixelInset.y, 0, soldierLoadBar.pixelInset.height);
    }

    public bool LoadBarCaptain(float speed, float time)
    {
        float value = speed == 0 ? captainLoadBar.pixelInset.width - (captainBoostDecreaseFactor * time) : captainLoadBar.pixelInset.width + Mathf.Abs(speed * time);
        float clamp = Mathf.Clamp(value, 0, captainBox.pixelInset.width);

        captainLoadBar.pixelInset = new Rect(captainLoadBar.pixelInset.x, captainLoadBar.pixelInset.y, clamp, captainLoadBar.pixelInset.height);

        return (clamp == captainBox.pixelInset.width);
    }

    public void LoadBarMechanic(float cooldown, float adder)
    {
        float slice = (mechanicBox.pixelInset.width / cooldown) * adder;
        float value = mechanicLoadBar.pixelInset.width + slice;
        float clamp = Mathf.Clamp(value, 0, mechanicBox.pixelInset.width);

        mechanicLoadBar.pixelInset = new Rect(mechanicLoadBar.pixelInset.x, mechanicLoadBar.pixelInset.y, clamp, mechanicLoadBar.pixelInset.height);
    }

    public void LoadBarSoldier(float cooldown, float adder)
    {
        float slice = (soldierBox.pixelInset.width / cooldown) * adder;
        float value = soldierLoadBar.pixelInset.width + slice;
        float clamp = Mathf.Clamp(value, 0, soldierBox.pixelInset.width);

        soldierLoadBar.pixelInset = new Rect(soldierLoadBar.pixelInset.x, soldierLoadBar.pixelInset.y, clamp, soldierLoadBar.pixelInset.height);
    }
}