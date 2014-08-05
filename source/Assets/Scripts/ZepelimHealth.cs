using UnityEngine;
using System.Collections;

public class ZepelimHealth : EnemyHealth {

    void Update()
    {
        if (health <= 0)
        {
            SendMessage("IncreaseSpeed");
        }
    }
}
