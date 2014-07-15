using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrewStatus : MonoBehaviour
{
    private Camera mainCamera;

    [SerializeField]
    private Transform captainHUD;
    [SerializeField]
    private Transform mechanicHUD;
    [SerializeField]
    private Transform soldierHUD;

    void Start()
    {
        mainCamera = Camera.main;

        captainHUD.position = mainCamera.ScreenToViewportPoint(new Vector3(0, 0, 0));
        mechanicHUD.position = mainCamera.ScreenToViewportPoint(new Vector3(0, 0, 0));
        soldierHUD.position = mainCamera.ScreenToViewportPoint(new Vector3(0, 0, 0));
    }
}