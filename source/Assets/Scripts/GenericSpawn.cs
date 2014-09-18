using UnityEngine;
using System.Collections;

public class GenericSpawn : MonoBehaviour
{
    public MovementSide side = MovementSide.RIGHTorUP;

    protected float yTranslate = 1f;
    [SerializeField]
    protected TagName tagName;
    [SerializeField]
    protected LayerName layerName;
    [SerializeField]
    protected float xOffset = 5f;
    [SerializeField]
    protected float yOffset = 5f;

    protected Vector2 StartPosition()
    {
        Camera mainCamera = Camera.main;
        Vector2 currentCameraPosition = mainCamera.transform.position;

        float direction = (int)side;

        Vector2 position = new Vector2();
        position.x = currentCameraPosition.x + (direction * mainCamera.aspect * mainCamera.orthographicSize) + (direction * xOffset);
        position.y = (currentCameraPosition.y + mainCamera.orthographicSize) / 2;// +yOffset;

        return position;
    }

    void Update()
    {
        transform.TranslateTo(0, yTranslate, 0, Time.deltaTime);
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
        yTranslate *= -1;
    }
}