using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// Singleton.
    /// </summary>
    private static CameraShake instance;

    /// <summary>
    /// Original position.
    /// </summary>
    private Vector3 originPosition;

    /// <summary>
    /// Original rotation.
    /// </summary>
    private Quaternion originRotation;

    /// <summary>
    /// Shake variables.
    /// </summary>
    private float shakeDecay = 0.001f;
    private float shakeIntensity;
    private float shakeCoefIntensity = 0.01f;

    #endregion

    #region Properties

    public static CameraShake Instance
    {
        get
        {
            if (CameraShake.instance == null)
            {
                CameraShake.instance = Camera.main.GetComponent<CameraShake>();
            }

            return CameraShake.instance;
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Update game logic.
    /// </summary>
    void Update()
    {
        /// When shake intensity is bigger than zero.
        if (shakeIntensity > 0)
        {
            transform.position = originPosition + Random.insideUnitSphere * shakeIntensity;

            transform.rotation = new Quaternion(
                        originRotation.x + Random.Range(-shakeIntensity, shakeIntensity) * .2f,
                        originRotation.y + Random.Range(-shakeIntensity, shakeIntensity) * .2f,
                        originRotation.z + Random.Range(-shakeIntensity, shakeIntensity) * .2f,
                        originRotation.w + Random.Range(-shakeIntensity, shakeIntensity) * .2f);

            shakeIntensity -= shakeDecay;
        }
    }

    /// <summary>
    /// Start to shake.
    /// </summary>
    public void Shake()
    {
        originPosition = transform.position;
        originRotation = transform.rotation;
        shakeIntensity = shakeCoefIntensity;
    }

    #endregion
}