using UnityEngine;
using System.Collections;

public enum MovementSide
{
    NONE = 0,
    LEFTorDOWN = -1,
    RIGHTorUP = 1,
}

public enum SpawnStatus
{
    INACTIVE,
    ACTIVE
}

public class GenericMovement : MonoBehaviour
{
    [SerializeField]
    public float horizontalSpeed;
    [SerializeField]
    protected float verticalSpeed;
    [SerializeField]
    public MovementSide side = MovementSide.LEFTorDOWN;

    protected Camera mainCamera;

    private float originalHorizontalSpeed;
    private float originalVerticalSpeed;    

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        Updating();
    }

    protected void Updating()
    {
        if (side == MovementSide.LEFTorDOWN)
        {
            LeftOrDownMovement();
        }
        else if (side == MovementSide.RIGHTorUP)
        {
            RightOrUpMovement();
        }
    }

    protected void Initialize()
    {
        mainCamera = Camera.main;

        originalHorizontalSpeed = horizontalSpeed;
        originalVerticalSpeed = verticalSpeed;

        RestartSpeed();
    }

    protected void RestartSpeed()
    {
        horizontalSpeed = originalHorizontalSpeed;
        verticalSpeed = originalVerticalSpeed;
    }

    protected virtual void TranslateLeftOrDown()
    {
        transform.TranslateTo(-horizontalSpeed, -verticalSpeed, 0, Time.deltaTime);
    }

    protected virtual void TranslateRightOrDown()
    {
        transform.TranslateTo(horizontalSpeed, verticalSpeed, 0, Time.deltaTime);
    }

    private void LeftOrDownMovement()
    {
        float xLimit = mainCamera.transform.position.x - (mainCamera.aspect * mainCamera.orthographicSize);
        float yLimit = mainCamera.transform.position.y - mainCamera.orthographicSize;

        if (!renderer.isVisible && (renderer.bounds.max.x < xLimit || renderer.bounds.max.y < yLimit))
        {
            gameObject.SetActive(false);
        }

        TranslateLeftOrDown();
    }

    private void RightOrUpMovement()
    {
        float xLimit = mainCamera.transform.position.x + (mainCamera.aspect * mainCamera.orthographicSize);
        float yLimit = mainCamera.transform.position.y + mainCamera.orthographicSize;

        if (!renderer.isVisible && (renderer.bounds.min.x > xLimit || renderer.bounds.min.y > yLimit))
        {
            gameObject.SetActive(false);
        }

        TranslateRightOrDown();
    }
}