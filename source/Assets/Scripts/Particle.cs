using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour
{
    public void InvokeDeactivate()
    {
        Invoke("Deactivate", particleSystem.duration);
    }

    private void Deactivate()
    {
        transform.parent = null;
        gameObject.SetActive(false);
    }
}
