using UnityEngine;
using System.Collections;

public class WaterSplash : MonoBehaviour
{
    void Update()
    {
        transform.Translate(transform.up * 5 * Time.deltaTime);
    }
}
