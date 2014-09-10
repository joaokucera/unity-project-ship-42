using UnityEngine;
using System.Collections;

public class SafeBuoy : MonoBehaviour
{
    [SerializeField]
    private TagName tagName = TagName.SafeBuoyLimit;
    [SerializeField]
    private LayerName layerName = LayerName.SafeBuoyLimit;
    [SerializeField]
    private float timeToRestart = 20f;

    private float xOffset = 1.15f;
    private float xTranslate = 2f;
    private bool isMoving = false;
    private Vector2 startPosition;

    void Start()
    {
        Camera mainCamera = Camera.main;

        Vector2 position = new Vector2();
        position.x = mainCamera.aspect * mainCamera.orthographicSize;
        position.y = mainCamera.orthographicSize;

        transform.position = new Vector2(position.x * xOffset,
                                        -position.y + renderer.bounds.size.y * xOffset);

        transform.parent.CreateTrigger(
            string.Format("{0} Safe Buoy Trigger Up", MovementSide.LEFTorDOWN), new Vector2(position.x / 2, transform.position.y),
            tagName.ToString(), layerName.ToString());

        transform.parent.CreateTrigger(
            string.Format("{0} Safe Buoy Trigger Down", MovementSide.RIGHTorUP), transform.position + new Vector3(xOffset * 5, 0, 0),
            tagName.ToString(), layerName.ToString());

        InvokeRepeating("Restart", timeToRestart, timeToRestart);

        startPosition = transform.position;
    }

    void Update()
    {
        if (isMoving)
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
                isMoving = false;
            }
        }
    }

    private void ReverseTranslate()
    {
        xTranslate *= -1;
    }

    private void Restart()
    {
        isMoving = true;
    }

    public void Replace()
    {
        transform.position = startPosition;
    }
}