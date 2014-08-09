using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour
{
    public static Vector2 MouseAction()
    {
        // Just 1 tap.
        if (Input.GetButtonDown("Fire1"))
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        return Vector2.zero;
    }

    public static Vector2 TouchAction()
    {
        // Just 1 tap.
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                return Camera.main.ScreenToWorldPoint(touch.position);
            }
        }

        return Vector2.zero;
    }
}
