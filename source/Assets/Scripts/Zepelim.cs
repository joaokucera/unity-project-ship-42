using UnityEngine;
using System.Collections;

public class Zepelim : Enemy
{
    [SerializeField]
    private float increaseTorque = 2f;
    [SerializeField]
    private float increaseSpeed = 20f;

    private float lastHorizontalSpeed = 0;

    void OnBecameVisible()
    {
        horizontalSpeed = lastHorizontalSpeed == 0 ?
                          originalHorizontalSpeed + (increaseSpeed * Time.deltaTime) :
                          lastHorizontalSpeed + (increaseSpeed * Time.deltaTime);

        lastHorizontalSpeed = horizontalSpeed;
    }

    public void IncreaseTorque()
    {
        horizontalSpeed += increaseTorque;
    }
}
