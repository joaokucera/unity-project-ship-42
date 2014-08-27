using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour
{
    public void InvokeDeactivate()
    {
        float duration = 0;

        if (particleSystem != null)
        {
            duration = particleSystem.duration;
        }
        else if (particleEmitter != null)
        {
            duration = particleEmitter.maxEnergy;
        }

        Invoke("Deactivate", duration);
    }

    private void Deactivate()
    {
        transform.parent = null;
        gameObject.SetActive(false);
    }
}
