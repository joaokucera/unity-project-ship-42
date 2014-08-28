using UnityEngine;
using System.Collections;

public class SafeBuoy : MonoBehaviour
{
    [SerializeField]
    private TagName tagName;
    [SerializeField]
    private LayerName layerName;

    private float xOffset = 1.15f;
    private float xTranslate = 2f;

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
            string.Format("{0} Safe Buoy Trigger Down", MovementSide.RIGHTorUP), transform.position,
            tagName.ToString(), layerName.ToString());
    }

    void Update()
    {
        transform.TranslateTo(xTranslate, 0, 0, Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == tagName.ToString())
        {
            ReverseTranslate();
        }
    }

    public void ReverseTranslate()
    {
        xTranslate *= -1;
    }
}