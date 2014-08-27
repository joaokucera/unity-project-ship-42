using UnityEngine;
using System.Collections;
using System;

public class Controls : MonoBehaviour
{
    public static bool MouseAction(ref Vector2 position)
    {
        // Just 1 tap.
        if (Input.GetButtonDown("Fire1"))
        {
            position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            return true;
        }

        return false;
    }

    public static bool TouchAction(ref Vector2 position)
    {
        // Just 1 tap.
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                position = Camera.main.ScreenToWorldPoint(touch.position);

                return true;
            }
        }

        return false;
    }
}
