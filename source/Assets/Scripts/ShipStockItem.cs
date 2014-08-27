using UnityEngine;
using System.Collections;

public class ShipStockItem : MonoBehaviour
{
    [SerializeField]
    private ModalSafeBuoy modalSafeBuoyScript;

    [HideInInspector]
    public int redCrossAmount, wholePizzaAmount, waterMellonAmount, chickenAmount, hamburguerAmount, slicePizzaAmount, cookieAmount, cokeAmount;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Safe Buoy")
        {
            modalSafeBuoyScript.OnVisible();
        }
    }

    public void AddItem(FriendItem friendItem)
    {
        switch (friendItem)
        {
            case FriendItem.RedCross:
                redCrossAmount++;
                break;
            case FriendItem.WholePizza:
                wholePizzaAmount++;
                break;
            case FriendItem.WaterMellon:
                waterMellonAmount++;
                break;
            case FriendItem.Chicken:
                chickenAmount++;
                break;
            case FriendItem.Hamburguer:
                hamburguerAmount++;
                break;
            case FriendItem.SlicePizza:
                slicePizzaAmount++;
                break;
            case FriendItem.Cookie:
                cookieAmount++;
                break;
            case FriendItem.Coke:
                cokeAmount++;
                break;
        }
    }
}
