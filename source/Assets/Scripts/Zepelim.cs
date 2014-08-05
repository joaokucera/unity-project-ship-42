using UnityEngine;
using System.Collections;

public class Zepelim : Enemy
{
    [SerializeField]
    private float increaseTorque = 2f;
    [SerializeField]
    private float increaseSpeed = 20f;

    //void OnBecameInVisible()
    //{
    //        foreach (Transform chield in transform)
    //        {
    //            if (chield.tag == "Target")
    //            {
    //                chield.parent = null;
    //            }
    //        }
    //}

    public void IncreaseTorque()
    {
        horizontalSpeed += increaseTorque;
    }

    public void IncreaseSpeed()
    {
        horizontalSpeed += increaseSpeed * Time.deltaTime;
    }
}
