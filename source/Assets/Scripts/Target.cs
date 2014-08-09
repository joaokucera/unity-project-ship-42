using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

    public void Deactivate()
    {
        transform.parent = null;
        gameObject.SetActive(false);
    }
}
