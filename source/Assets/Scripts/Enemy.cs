using UnityEngine;
using System.Collections;
using System.Linq;

public class Enemy : GenericMovement, IEnemy
{
    public void TurnBack()
    {
        RestartSpeed();

        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        int aux = (int)side * -1;
        side = (MovementSide)aux;

        gameObject.SetActive(true);
    }
}