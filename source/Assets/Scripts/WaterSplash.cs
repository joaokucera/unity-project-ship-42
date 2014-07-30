using UnityEngine;
using System.Collections;

public class WaterSplash : MonoBehaviour
{
    private void Disable()
    {
         transform.parent.gameObject.SetActive(false);
    }
}
