using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FriendBoxValues
{
    private static FriendBoxValues Instance;
    private static Dictionary<FriendItem, int> ItemValues;

    private FriendBoxValues()
    {
        ItemValues = new Dictionary<FriendItem, int>();

        FillItemsValues();
    }

    public static FriendBoxValues GetInstance()
    {
        if (Instance == null)
        {
            Instance = new FriendBoxValues();
        }

        return Instance;
    }

    private static void FillItemsValues()
    {
        ItemValues.Add(FriendItem.Red_Cross, 10);
        ItemValues.Add(FriendItem.Whole_Pizza, 9);
        ItemValues.Add(FriendItem.Watermelon, 8);
        ItemValues.Add(FriendItem.Chicken, 6);
        ItemValues.Add(FriendItem.Hamburguer, 4);
        ItemValues.Add(FriendItem.Slice_Pizza, 3);
        ItemValues.Add(FriendItem.Cookie, 2);
        ItemValues.Add(FriendItem.Coke, 1);
    }

    public void GetValue(FriendItem friendItem, ref float stamina)
    {
        int value;

        if (ItemValues.TryGetValue(friendItem, out value))
        {
            float increasedStamina = stamina + value;

            stamina = Mathf.Clamp(increasedStamina, 0, 99);
        }
    }

    public float[] GetWholePizza(FriendItem friendItem, float[] staminas)
    {
        int value;

        if (ItemValues.TryGetValue(friendItem, out value))
        {
            int splitedValue = value / 3;

            for (int i = 0; i < staminas.Length; i++)
            {
                float increasedStamina = staminas[i] + splitedValue;

                staminas[i] = Mathf.Clamp(increasedStamina, 0, 99);
            }
        }

        return staminas;
    }
}
