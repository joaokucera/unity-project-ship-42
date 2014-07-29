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
                CrewStatus.instance = GameObject.Find("Crew Status").GetComponent<CrewStatus>();
            }

            return CrewStatus.instance;
        }
    }

    private Camera mainCamera;

    [SerializeField]
    private GUITexture captainTexture = null, mechanicTexture = null, soldierTexture = null;
    [SerializeField]
    private GUIText captainText = null, mechanicText = null, soldierText = null;
    [SerializeField]
    public float captainStamina = 50, mechanicStamina = 50, soldierStamina = 50;
    [SerializeField]
    private GUITexture captainBox = null, mechanicBox = null, soldierBox = null;
    [SerializeField]
    private GUITexture captainLoadBar = null, mechanicLoadBar = null, soldierLoadBar = null;

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
        captainTexture.transform.position = new Vector2(viewport.x + 0.05f, viewport.y - 0.075f);
        mechanicTexture.transform.position = new Vector2(viewport.x + 0.15f, viewport.y - 0.075f);
        soldierTexture.transform.position = new Vector2(viewport.x + 0.25f, viewport.y - 0.075f);

        // HUD Crew Health.
        captainText.transform.position = new Vector2(viewport.x + 0.05f, viewport.y - 0.125f);
        mechanicText.transform.position = new Vector2(viewport.x + 0.15f, viewport.y - 0.125f);
        soldierText.transform.position = new Vector2(viewport.x + 0.25f, viewport.y - 0.125f);

        // HUD Crew Box.
        captainBox.transform.position = new Vector2(viewport.x + 0.05f, viewport.y - 0.03f);
        mechanicBox.transform.position = new Vector2(viewport.x + 0.15f, viewport.y - 0.03f);
        soldierBox.transform.position = new Vector2(viewport.x + 0.25f, viewport.y - 0.03f);

        // HUD Crew Load Bar.
        captainLoadBar.transform.position = new Vector2(viewport.x + 0.05f, viewport.y - 0.03f);
        mechanicLoadBar.transform.position = new Vector2(viewport.x + 0.15f, viewport.y - 0.03f);
        soldierLoadBar.transform.position = new Vector2(viewport.x + 0.25f, viewport.y - 0.03f);
    }

    void Update()
    {
        captainText.text = string.Format("{0}/100", captainStamina);
        mechanicText.text = string.Format("{0}/100", mechanicStamina);
        soldierText.text = string.Format("{0}/100", soldierStamina);

        //SetCaptainStamina();
        //SetMechanicStamina();
        //SetSoldierStamina();
    }

    //private void SetCaptainStamina()
    //{
    //    float value = loadBarFactor * captainStamina;

    //    float clamp = Mathf.Clamp(value, 0, captainBox.pixelInset.width);

    //    captainLoadBar.pixelInset = new Rect(captainLoadBar.pixelInset.x, captainLoadBar.pixelInset.y, clamp, captainLoadBar.pixelInset.height);
    //}

    //private void SetMechanicStamina()
    //{
    //    float value = loadBarFactor * mechanicStamina;

    //    float clamp = Mathf.Clamp(value, 0, mechanicBox.pixelInset.width);

    //    mechanicLoadBar.pixelInset = new Rect(mechanicLoadBar.pixelInset.x, mechanicLoadBar.pixelInset.y, clamp, mechanicLoadBar.pixelInset.height);
    //}

    //private void SetSoldierStamina()
    //{
    //    float value = loadBarFactor * soldierStamina;

    //    float clamp = Mathf.Clamp(value, 0, soldierBox.pixelInset.width);

    //    soldierLoadBar.pixelInset = new Rect(soldierLoadBar.pixelInset.x, soldierLoadBar.pixelInset.y, clamp, soldierLoadBar.pixelInset.height);
    //}

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

    public void LoadBarCaptain(float cooldown, float adder)
    {
        float slice = (captainBox.pixelInset.width / cooldown) * adder;

        float value = captainLoadBar.pixelInset.width + slice;

        float clamp = Mathf.Clamp(value, 0, captainBox.pixelInset.width);

        captainLoadBar.pixelInset = new Rect(captainLoadBar.pixelInset.x, captainLoadBar.pixelInset.y, clamp, captainLoadBar.pixelInset.height);
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