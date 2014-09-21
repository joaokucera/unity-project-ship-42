using UnityEngine;
using System.Collections;

public enum SafeBuoyStatus
{
    Waiting,
    Moving
}

public class SafeBuoy : MonoBehaviour
{
    [SerializeField]
    private TagName tagName = TagName.SafeBuoyLimit;
    [SerializeField]
    private LayerName layerName = LayerName.SafeBuoyLimit;

    /// <summary>
    /// BALANCE: Spawn da bóia salva vidas a cada 42 segundos.
    /// </summary>
    private float respawnTime = 42f;

    private float xOffset = 1.15f;
    private float xTranslate = -2f;
    private Vector2 startPosition;

    private SafeBuoyStatus status = SafeBuoyStatus.Waiting;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        Vector2 position = new Vector2();
        position.x = mainCamera.aspect * mainCamera.orthographicSize;
        position.y = mainCamera.orthographicSize;

        transform.position = new Vector2(position.x * xOffset,
                                        -position.y + renderer.bounds.size.y * xOffset);

        transform.parent.CreateTrigger(
            string.Format("{0} Safe Buoy Trigger Left", MovementSide.LEFTorDOWN), new Vector2(position.x / 2, transform.position.y),
            tagName.ToString(), layerName.ToString());

        transform.parent.CreateTrigger(
            string.Format("{0} Safe Buoy Trigger Right", MovementSide.RIGHTorUP), transform.position + new Vector3(xOffset * 5, 0, 0),
            tagName.ToString(), layerName.ToString());

        Invoke("Restart", respawnTime);

        startPosition = transform.position;
    }

    void Update()
    {
        if (status == SafeBuoyStatus.Moving)
        {
            transform.TranslateTo(xTranslate, 0, 0, Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == tagName.ToString())
        {
            ReverseTranslate();

            if (collider.name.Contains("RIGHT"))
            {
                ToWait();
            }
        }
    }

    private void ReverseTranslate()
    {
        xTranslate *= -1;
    }

    private void Restart()
    {
        status = SafeBuoyStatus.Moving;

        SoundEffectScript.Instance.PlaySound(SoundEffectClip.EnemyShowingSound);

        CancelInvoke("Restart");
    }

    private void ToWait()
    {
        status = SafeBuoyStatus.Waiting;
     
        Invoke("Restart", respawnTime);
    }

    public void Replace()
    {
        transform.position = startPosition;

        ToWait();
    }
}