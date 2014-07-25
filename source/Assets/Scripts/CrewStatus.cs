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
    public int captainStamina = 50, mechanicStamina = 50, soldierStamina = 50;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        mainCamera = Camera.main;

        Vector2 viewport = mainCamera.WorldToViewportPoint(new Vector2(-mainCamera.aspect * mainCamera.orthographicSize, mainCamera.orthographicSize));

        captainTexture.transform.position = new Vector2(viewport.x + 0.05f, viewport.y - 0.075f);
        mechanicTexture.transform.position = new Vector2(viewport.x + 0.15f, viewport.y - 0.075f);
        soldierTexture.transform.position = new Vector2(viewport.x + 0.25f, viewport.y - 0.075f);

        captainText.transform.position = new Vector2(viewport.x + 0.05f, viewport.y - 0.125f);
        mechanicText.transform.position = new Vector2(viewport.x + 0.15f, viewport.y - 0.125f);
        soldierText.transform.position = new Vector2(viewport.x + 0.25f, viewport.y - 0.125f);
    }

    void Update()
    {
        captainText.text = string.Format("{0}/100", captainStamina);
        mechanicText.text = string.Format("{0}/100", mechanicStamina);
        soldierText.text = string.Format("{0}/100", soldierStamina);
    }
}