using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalSafeBuoy : MonoBehaviour
{
    private const string WarningChooseAnItem = "First choose which item will be used!";
    private const string WarningThereIsNoItem = "There is no {0} item available!";
    private const string WarningWholePizza = "A whole pizza improves all crew equally!";

    [SerializeField]
    private ShipStockItem shipStockItem = null;
    [SerializeField]
    private GameObject pauseObject = null;

    [SerializeField]
    private GameObject playerShotSpawnerObject = null, crewStatusObject = null;

    [SerializeField]
    private GUIText warningGuiText = null;

    [SerializeField]
    private SpriteRenderer giftItemSelected = null, crewPersonSelected = null;

    [SerializeField]
    private SpriteRenderer closeButton = null, captainRenderer = null, mechanicRenderer = null, soldierRenderer = null,
                           redCrossRenderer = null, watermelonRenderer = null, chickenRenderer = null, hamburgerRenderer = null,
                           pizzaWholeRenderer = null, pizzaSliceRenderer = null, cookieRenderer = null, cokeRenderer = null;
    [SerializeField]
    private GUIText captainText = null, mechanicText = null, soldierText = null,
                    redCrossText = null, watermelonText = null, chickenText = null, hamburgerText = null,
                    pizzaWholeText = null, pizzaSliceText = null, cookieText = null, cokeText = null;

    private Camera mainCamera;

    private FriendItem currentFriendItem;
    private float timeToWarningTextHide;
    private float LimitTimeToWarningTextHide = 2.5f;
    private float lastDeltaTime;

    void Start()
    {
        warningGuiText.enabled = false;
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
        crewStatusObject.SetActive(false);

        pauseObject.SetActive(false);

        Time.timeScale = 0f;
    }

    private void OnInvisible()
    {
        gameObject.SetActive(false);
        warningGuiText.enabled = false;

        playerShotSpawnerObject.SetActive(true);
        crewStatusObject.SetActive(true);

        pauseObject.SetActive(true);

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Time.deltaTime > 0)
        {
            lastDeltaTime = Time.deltaTime;
        }

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

        UpdateTextItems();

        if (warningGuiText.enabled)
        {
            timeToWarningTextHide += lastDeltaTime;
            if (timeToWarningTextHide >= LimitTimeToWarningTextHide)
            {
                warningGuiText.enabled = false;
            }
        }
        else
        {
            timeToWarningTextHide = 0f;
        }
    }

    private void UpdateTextItems()
    {
        captainText.text = CrewStatus.Instance.captainStamina.ToString("00");
        mechanicText.text = CrewStatus.Instance.mechanicStamina.ToString("00");
        soldierText.text = CrewStatus.Instance.soldierStamina.ToString("00");

        redCrossText.text = shipStockItem.redCrossAmount.ToString();
        watermelonText.text = shipStockItem.waterMellonAmount.ToString();
        chickenText.text = shipStockItem.chickenAmount.ToString();
        hamburgerText.text = shipStockItem.hamburguerAmount.ToString();
        pizzaWholeText.text = shipStockItem.wholePizzaAmount.ToString();
        pizzaSliceText.text = shipStockItem.slicePizzaAmount.ToString();
        cookieText.text = shipStockItem.cookieAmount.ToString();
        cokeText.text = shipStockItem.cokeAmount.ToString();
    }

    public void CheckAction(Vector2 position)
    {
        if (position.HasActivated(closeButton.transform.position, closeButton.bounds.size, true, true))
        {
            OnInvisible();
        }

        bool thereIsNoItem = false;

        // Selected ITEM.
        if (position.HasActivated(redCrossRenderer.transform.position, redCrossRenderer.bounds.size))
        {
            currentFriendItem = FriendItem.Red_Cross;

            if (shipStockItem.redCrossAmount > 0)
            {
                giftItemSelected.transform.position = redCrossRenderer.transform.position;
                giftItemSelected.enabled = true;
            }
            else
            {
                thereIsNoItem = true;
            }
        }
        else if (position.HasActivated(watermelonRenderer.transform.position, watermelonRenderer.bounds.size))
        {
            currentFriendItem = FriendItem.Watermelon;

            if (shipStockItem.waterMellonAmount > 0)
            {
                giftItemSelected.transform.position = watermelonRenderer.transform.position;
                giftItemSelected.enabled = true;
            }
            else
            {
                thereIsNoItem = true;
            }
        }
        else if (position.HasActivated(chickenRenderer.transform.position, chickenRenderer.bounds.size))
        {
            currentFriendItem = FriendItem.Chicken;

            if (shipStockItem.chickenAmount > 0)
            {
                giftItemSelected.transform.position = chickenRenderer.transform.position;
                giftItemSelected.enabled = true;
            }
            else
            {
                thereIsNoItem = true;
            }
        }
        else if (position.HasActivated(hamburgerRenderer.transform.position, hamburgerRenderer.bounds.size))
        {
            currentFriendItem = FriendItem.Hamburguer;

            if (shipStockItem.hamburguerAmount > 0)
            {
                giftItemSelected.transform.position = hamburgerRenderer.transform.position;
                giftItemSelected.enabled = true;
            }
            else
            {
                thereIsNoItem = true;
            }
        }
        else if (position.HasActivated(pizzaWholeRenderer.transform.position, pizzaWholeRenderer.bounds.size))
        {
            currentFriendItem = FriendItem.Whole_Pizza;

            if (shipStockItem.wholePizzaAmount > 0)
            {
                giftItemSelected.transform.position = pizzaWholeRenderer.transform.position;
                giftItemSelected.enabled = true;

                warningGuiText.text = WarningWholePizza;
                warningGuiText.enabled = true;
            }
            else
            {
                thereIsNoItem = true;
            }
        }
        else if (position.HasActivated(pizzaSliceRenderer.transform.position, pizzaSliceRenderer.bounds.size))
        {
            currentFriendItem = FriendItem.Slice_Pizza;

            if (shipStockItem.slicePizzaAmount > 0)
            {
                giftItemSelected.transform.position = pizzaSliceRenderer.transform.position;
                giftItemSelected.enabled = true;
            }
            else
            {
                thereIsNoItem = true;
            }
        }
        else if (position.HasActivated(cookieRenderer.transform.position, cookieRenderer.bounds.size))
        {
            currentFriendItem = FriendItem.Cookie;

            if (shipStockItem.cookieAmount > 0)
            {
                giftItemSelected.transform.position = cookieRenderer.transform.position;
                giftItemSelected.enabled = true;
            }
            else
            {
                thereIsNoItem = true;
            }
        }
        else if (position.HasActivated(cokeRenderer.transform.position, cokeRenderer.bounds.size))
        {
            currentFriendItem = FriendItem.Coke;

            if (shipStockItem.cokeAmount > 0)
            {
                giftItemSelected.transform.position = cokeRenderer.transform.position;
                giftItemSelected.enabled = true;
            }
            else
            {
                thereIsNoItem = true;
            }
        }

        // If there is no item available.
        if (thereIsNoItem)
        {
            print(thereIsNoItem);
            warningGuiText.text = string.Format(WarningThereIsNoItem, currentFriendItem.ToString().ToLower().Replace("_", " "));
            warningGuiText.enabled = true;

            return;
        }

        // Selected CREW.
        bool enableDefaultWarning = false;
        bool increased = false;

        if (position.HasActivated(captainRenderer.transform.position, captainRenderer.bounds.size))
        {
            if (giftItemSelected.enabled == true)
            {
                warningGuiText.enabled = false;

                if (currentFriendItem != FriendItem.Whole_Pizza)
                {
                    FriendBoxValues.GetInstance().GetValue(currentFriendItem, ref CrewStatus.Instance.captainStamina);
                }

                increased = true;

                crewPersonSelected.transform.position = captainRenderer.transform.position;
                crewPersonSelected.enabled = true;

                giftItemSelected.enabled = false;
            }
            else
            {
                enableDefaultWarning = true;
            }
        }
        else if (position.HasActivated(mechanicRenderer.transform.position, mechanicRenderer.bounds.size))
        {
            if (giftItemSelected.enabled == true)
            {
                warningGuiText.enabled = false;

                if (currentFriendItem != FriendItem.Whole_Pizza)
                {
                    FriendBoxValues.GetInstance().GetValue(currentFriendItem, ref CrewStatus.Instance.mechanicStamina);
                }

                increased = true;

                crewPersonSelected.transform.position = mechanicRenderer.transform.position;
                crewPersonSelected.enabled = true;

                giftItemSelected.enabled = false;
            }
            else
            {
                enableDefaultWarning = true;
            }
        }
        else if (position.HasActivated(soldierRenderer.transform.position, soldierRenderer.bounds.size))
        {
            if (giftItemSelected.enabled == true)
            {
                warningGuiText.enabled = false;

                if (currentFriendItem != FriendItem.Whole_Pizza)
                {
                    FriendBoxValues.GetInstance().GetValue(currentFriendItem, ref CrewStatus.Instance.soldierStamina);
                }

                crewPersonSelected.transform.position = soldierRenderer.transform.position;
                crewPersonSelected.enabled = true;

                giftItemSelected.enabled = false;
            }
            else
            {
                enableDefaultWarning = true;
            }
        }

        if (increased)
        {
            if (currentFriendItem == FriendItem.Whole_Pizza)
            {
                var staminas = FriendBoxValues.GetInstance().GetWholePizza(currentFriendItem, new float[] 
                    { 
                        CrewStatus.Instance.captainStamina, 
                        CrewStatus.Instance.mechanicStamina, 
                        CrewStatus.Instance.soldierStamina
                    });

                CrewStatus.Instance.captainStamina = staminas[0];
                CrewStatus.Instance.mechanicStamina = staminas[1];
                CrewStatus.Instance.soldierStamina = staminas[2];
            }

            RemoveItem(currentFriendItem);
        }

        if (enableDefaultWarning)
        {
            warningGuiText.text = WarningChooseAnItem;
            warningGuiText.enabled = true;
        }
    }

    private void RemoveItem(FriendItem friendItem)
    {
        switch (friendItem)
        {
            case FriendItem.Red_Cross:
                shipStockItem.redCrossAmount--;
                break;
            case FriendItem.Whole_Pizza:
                shipStockItem.wholePizzaAmount--;
                break;
            case FriendItem.Watermelon:
                shipStockItem.waterMellonAmount--;
                break;
            case FriendItem.Chicken:
                shipStockItem.chickenAmount--;
                break;
            case FriendItem.Hamburguer:
                shipStockItem.hamburguerAmount--;
                break;
            case FriendItem.Slice_Pizza:
                shipStockItem.slicePizzaAmount--;
                break;
            case FriendItem.Cookie:
                shipStockItem.cookieAmount--;
                break;
            case FriendItem.Coke:
                shipStockItem.cokeAmount--;
                break;
        }
    }

    //private bool HasActivated(Vector2 positionA, Vector2 positionB, Vector2 size)
    //{
    //    bool hasActivated = Mathf.Abs(positionA.x - positionB.x) <= size.x &&
    //                        Mathf.Abs(positionA.y - positionB.y) <= size.y;

    //    if (hasActivated)
    //    {
    //        SoundEffectScript.Instance.PlaySound(SoundEffectClip.ClickButton);
    //    }

    //    return hasActivated;
    //}
}