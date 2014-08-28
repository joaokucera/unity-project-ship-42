using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class ModalSafeBuoy : MonoBehaviour
{
    [SerializeField]
    private GameObject playerShotSpawnerObject, crewStatusObject, warningObject;

    [SerializeField]
    private SpriteRenderer giftItemSelected, crewPersonSelected;

    private Camera mainCamera;

    [SerializeField]
    private SpriteRenderer captainRenderer = null, mechanicRenderer = null, soldierRenderer = null,
                           redCrossRenderer = null, watermelonRenderer = null, chickenRenderer = null, hamburgerRenderer = null,
                           pizzaWholeRenderer = null, pizzaSliceRenderer = null, cookieRenderer = null, cokeRenderer = null;
    [SerializeField]
    private GUIText captainText = null, mechanicText = null, soldierText = null,
                    redCrossText = null, watermelonText = null, chickenText = null, hamburgerText = null,
                    pizzaWholeText = null, pizzaSliceText = null, cookieText = null, cokeText = null;

    void Start()
    {
        gameObject.SetActive(false);
        warningObject.SetActive(false);

        giftItemSelected.enabled = false;
        crewPersonSelected.enabled = false;

        mainCamera = Camera.main;

        // CAPTAIN
        Vector3 captainSize = (captainRenderer.bounds.max - captainRenderer.bounds.min) * 1.15f;
        captainText.transform.position = mainCamera.WorldToViewportPoint(
            new Vector3(captainRenderer.transform.position.x + captainSize.x, captainRenderer.transform.position.y - captainSize.y / 6, captainRenderer.transform.position.z));
        // MECHANIC
        Vector3 mechanicSize = (mechanicRenderer.bounds.max - mechanicRenderer.bounds.min) * 1.15f;
        mechanicText.transform.position = mainCamera.WorldToViewportPoint(
            new Vector3(mechanicRenderer.transform.position.x + mechanicSize.x, mechanicRenderer.transform.position.y - mechanicSize.y / 6, mechanicRenderer.transform.position.z));
        // SOLDIER
        Vector3 soldierSize = (soldierRenderer.bounds.max - soldierRenderer.bounds.min) * 1.15f;
        soldierText.transform.position = mainCamera.WorldToViewportPoint(
            new Vector3(soldierRenderer.transform.position.x + soldierSize.x, soldierRenderer.transform.position.y - soldierSize.y / 6, soldierRenderer.transform.position.z));

        // REDCROSS
        Vector3 redCrossSize = redCrossRenderer.bounds.max - redCrossRenderer.bounds.min;
        redCrossText.transform.position = mainCamera.WorldToViewportPoint(
            new Vector3(redCrossRenderer.transform.position.x + redCrossSize.x, redCrossRenderer.transform.position.y - redCrossSize.y / 6, redCrossRenderer.transform.position.z));
        // WATERMELON
        Vector3 watermelonSize = watermelonRenderer.bounds.max - watermelonRenderer.bounds.min;
        watermelonText.transform.position = mainCamera.WorldToViewportPoint(
           new Vector3(watermelonRenderer.transform.position.x + watermelonSize.x, watermelonRenderer.transform.position.y - watermelonSize.y / 6, watermelonRenderer.transform.position.z));
        // CHICKEN
        Vector3 chickenSize = chickenRenderer.bounds.max - chickenRenderer.bounds.min;
        chickenText.transform.position = mainCamera.WorldToViewportPoint(
            new Vector3(chickenRenderer.transform.position.x + chickenSize.x, chickenRenderer.transform.position.y - chickenSize.y / 6, chickenRenderer.transform.position.z));
        // HAMBURGER
        Vector3 hamburgerSize = hamburgerRenderer.bounds.max - hamburgerRenderer.bounds.min;
        hamburgerText.transform.position = mainCamera.WorldToViewportPoint(
            new Vector3(hamburgerRenderer.transform.position.x + hamburgerSize.x, hamburgerRenderer.transform.position.y - hamburgerSize.y / 6, hamburgerRenderer.transform.position.z));
        // PIZZA WHOLE
        Vector3 pizzaWholeSize = pizzaWholeRenderer.bounds.max - pizzaWholeRenderer.bounds.min;
        pizzaWholeText.transform.position = mainCamera.WorldToViewportPoint(
            new Vector3(pizzaWholeRenderer.transform.position.x + pizzaWholeSize.x, pizzaWholeRenderer.transform.position.y - pizzaWholeSize.y / 6, pizzaWholeRenderer.transform.position.z));
        // PIZZA SLICE
        Vector3 pizzaSliceSize = pizzaSliceRenderer.bounds.max - pizzaSliceRenderer.bounds.min;
        pizzaSliceText.transform.position = mainCamera.WorldToViewportPoint(
            new Vector3(pizzaSliceRenderer.transform.position.x + pizzaSliceSize.x, pizzaSliceRenderer.transform.position.y - pizzaSliceSize.y / 6, pizzaSliceRenderer.transform.position.z));
        // COOKIE
        Vector3 cookieSize = cookieRenderer.bounds.max - cookieRenderer.bounds.min;
        cookieText.transform.position = mainCamera.WorldToViewportPoint(
            new Vector3(cookieRenderer.transform.position.x + cookieSize.x, cookieRenderer.transform.position.y - cookieSize.y / 6, pizzaSliceRenderer.transform.position.z));
        // COKE
        Vector3 cokeSize = cokeRenderer.bounds.max - cokeRenderer.bounds.min;
        cokeText.transform.position = mainCamera.WorldToViewportPoint(
            new Vector3(cokeRenderer.transform.position.x + cokeSize.x, cokeRenderer.transform.position.y - cokeSize.y / 6, pizzaSliceRenderer.transform.position.z));
    }

    public void OnVisible()
    {
        gameObject.SetActive(true);

        playerShotSpawnerObject.SetActive(false);
        foreach (Transform item in crewStatusObject.transform)
        {
            item.position = new Vector3(item.position.x, item.position.y, 1);
        }

        Time.timeScale = 0f;
    }

    void Update()
    {
        Vector2 position = Vector2.zero;

#if UNITY_EDITOR
        if (Controls.MouseAction(ref position))
        {
#else
        if (Controls.TouchAction(ref position))
        {
#endif
            CheckAction(position);
        }
    }

    public void CheckAction(Vector2 position)
    {
        // Selected ITEM.
        if (HasActivated(position, redCrossRenderer.transform.position, redCrossRenderer.bounds.size))
        {
            giftItemSelected.transform.position = redCrossRenderer.transform.position;
            giftItemSelected.enabled = true;
        }
        else if (HasActivated(position, watermelonRenderer.transform.position, watermelonRenderer.bounds.size))
        {
            giftItemSelected.transform.position = watermelonRenderer.transform.position;
            giftItemSelected.enabled = true;
        }
        else if (HasActivated(position, chickenRenderer.transform.position, chickenRenderer.bounds.size))
        {
            giftItemSelected.transform.position = chickenRenderer.transform.position;
            giftItemSelected.enabled = true;
        }
        else if (HasActivated(position, hamburgerRenderer.transform.position, hamburgerRenderer.bounds.size))
        {
            giftItemSelected.transform.position = hamburgerRenderer.transform.position;
            giftItemSelected.enabled = true;
        }
        else if (HasActivated(position, pizzaWholeRenderer.transform.position, pizzaWholeRenderer.bounds.size))
        {
            giftItemSelected.transform.position = pizzaWholeRenderer.transform.position;
            giftItemSelected.enabled = true;
        }
        else if (HasActivated(position, pizzaSliceRenderer.transform.position, pizzaSliceRenderer.bounds.size))
        {
            giftItemSelected.transform.position = pizzaSliceRenderer.transform.position;
            giftItemSelected.enabled = true;
        }
        else if (HasActivated(position, cookieRenderer.transform.position, cookieRenderer.bounds.size))
        {
            giftItemSelected.transform.position = cookieRenderer.transform.position;
            giftItemSelected.enabled = true;
        }
        else if (HasActivated(position, cokeRenderer.transform.position, cokeRenderer.bounds.size))
        {
            giftItemSelected.transform.position = cokeRenderer.transform.position;
            giftItemSelected.enabled = true;
        }

        // Selected CREW.
        if (giftItemSelected.enabled == true)
        {
            warningObject.SetActive(false);

            if (HasActivated(position, captainRenderer.transform.position, captainRenderer.bounds.size))
            {
                crewPersonSelected.transform.position = captainRenderer.transform.position;
                crewPersonSelected.enabled = true;
            }
            else if (HasActivated(position, mechanicRenderer.transform.position, mechanicRenderer.bounds.size))
            {
                crewPersonSelected.transform.position = mechanicRenderer.transform.position;
                crewPersonSelected.enabled = true;
            }
            else if (HasActivated(position, soldierRenderer.transform.position, soldierRenderer.bounds.size))
            {
                crewPersonSelected.transform.position = soldierRenderer.transform.position;
                crewPersonSelected.enabled = true;
            }
        }
        else
        {
            warningObject.SetActive(true);
        }
    }

    private bool HasActivated(Vector2 positionA, Vector2 positionB, Vector2 size)
    {
        return Mathf.Abs(positionA.x - positionB.x) <= size.x &&
               Mathf.Abs(positionA.y - positionB.y) <= size.y;
    }
}
