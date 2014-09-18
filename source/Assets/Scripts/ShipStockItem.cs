using UnityEngine;
using System.Collections;

public class ShipStockItem : MonoBehaviour
{
    [SerializeField]
    private ModalSafeBuoy modalSafeBuoyScript = null;

    [HideInInspector]
    public int redCrossAmount, wholePizzaAmount, waterMellonAmount, chickenAmount, hamburguerAmount, slicePizzaAmount, cookieAmount, cokeAmount;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Safe Buoy")
        {
            SoundEffectScript.Instance.PlaySound(SoundEffectClip.ShipTakeSafeBuoy);

            collider.SendMessage("Replace");

            modalSafeBuoyScript.OnVisible();
        }
    }

    public void AddItem(FriendItem friendItem)
    {
        switch (friendItem)
        {
            case FriendItem.Red_Cross:
                redCrossAmount++;
                break;
            case FriendItem.Whole_Pizza:
                wholePizzaAmount++;
                break;
            case FriendItem.Watermelon:
                waterMellonAmount++;
                break;
            case FriendItem.Chicken:
                chickenAmount++;
                break;
            case FriendItem.Hamburguer:
                hamburguerAmount++;
                break;
            case FriendItem.Slice_Pizza:
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
