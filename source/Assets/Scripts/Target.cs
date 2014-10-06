using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

    void OnBecameVisible()
    {
        Invoke("Deactivate", 1f);
    }

    public void Deactivate()
    {
        transform.parent = null;

        gameObject.SetActive(false);
    }
}
