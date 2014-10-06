using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour
{
    [SerializeField]
    private float speedIncreaseFactor = 100;
    [SerializeField]
    private Renderer leftParticleSplash = null, rightParticleSplash = null;
    [SerializeField]
    private GameOver gameOverScript;

    private float speed;
    private MovementSide movementSide = MovementSide.NONE;
    private Camera mainCamera;
    private Vector2 boundSize;
    private float fixedVerticalPosition;

    private bool keepMoving = true;
    private float increaseAccelerationFactor = 20f;

    void Start()
    {
        SoundEffectScript.Instance.PlaySound(SoundEffectClip.StartGame);

        mainCamera = Camera.main;
        boundSize = renderer.bounds.size / 2;

        float yPosition = -mainCamera.orthographicSize + (renderer.bounds.size.y / 3f);
        Vector2 startPosition = new Vector2(0, yPosition);
        transform.position = startPosition;
        fixedVerticalPosition = startPosition.y;

        leftParticleSplash.sortingLayerName = "Middleground";
        leftParticleSplash.sortingOrder = 0;
        leftParticleSplash.enabled = false;
        rightParticleSplash.sortingLayerName = "Middleground";
        rightParticleSplash.sortingOrder = 0;
        rightParticleSplash.enabled = false;
    }

    void Update()
    {
        if (keepMoving)
        {
            var movement = (int)movementSide * (CrewStatus.Instance.captainStamina / 10);
            if (CrewStatus.Instance.LoadBarCaptain(movement, Time.deltaTime))
            {
                speed += movement / speedIncreaseFactor;
                CrewStatus.Instance.CaptainBoost = true;

                if (movementSide == MovementSide.LEFTorDOWN)
                {
                    rightParticleSplash.enabled = true;
                }
                else if (movementSide == MovementSide.RIGHTorUP)
                {
                    leftParticleSplash.enabled = true;
                }
            }
            else
            {
                speed = movement;
                CrewStatus.Instance.CaptainBoost = false;

                leftParticleSplash.enabled = false;
                rightParticleSplash.enabled = false;
            }

            if (GameSettings.Instance.acceleratorEnabled)
            {
                speed = AcceleratorAction();
            }

            transform.TranslateTo(speed, 0, 0, Time.deltaTime);
            transform.position = new Vector2(transform.position.x, fixedVerticalPosition);

#if UNITY_EDITOR || UNITY_WEBPLAYER
            MouseAction();
#else
		TouchAction ();
#endif

            // Enforce ship inside the screen.
            EnforceBounds();
        }
        else
        {
            transform.TranslateTo(0, -1, 0, Time.deltaTime);
        }
    }

    public void Die()
    {
        keepMoving = false;

        Invoke("GoToScore", 5f);

        SoundEffectScript.Instance.PlaySound(SoundEffectClip.ShipFallingOcean);
    }

    private void GoToScore()
    {
        gameOverScript.OnVisible();

        //Application.LoadLevel("Score");

        CancelInvoke("GoToScore");
    }

    private void MouseAction()
    {
        // Just 1 tap.
        if (Input.GetButton("Fire1"))
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            Movement(mousePosition);
        }
        else
        {
            movementSide = MovementSide.NONE;
        }
    }

    private void TouchAction()
    {
        // Just 1 tap.
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                movementSide = MovementSide.NONE;
            }
            else
            {
                Movement(touchPosition);
            }
        }
        else
        {
            movementSide = MovementSide.NONE;
        }
    }

    private float AcceleratorAction()
    {
        Vector2 accelerationPosition = Vector2.zero;

        // Got acceleration.
        if (Input.accelerationEventCount > 0)
        {
            accelerationPosition.x = Input.acceleration.x;
            accelerationPosition.y = 0;

            if (accelerationPosition.sqrMagnitude > 1)
            {
                accelerationPosition.Normalize();
            }
        }

        return accelerationPosition.x * increaseAccelerationFactor;
    }

    private void Movement(Vector2 position)
    {
        if (position.x < transform.position.x)
        {
            movementSide = MovementSide.LEFTorDOWN;
        }
        else if (position.x > transform.position.x)
        {
            movementSide = MovementSide.RIGHTorUP;
        }
        else
        {
            movementSide = MovementSide.NONE;
        }
    }

    /// <summary>
    /// Enforces the bounds.
    /// </summary>
    private void EnforceBounds()
    {
        // Current positions.
        Vector2 currentPosition = transform.position;
        Vector2 currentCameraPosition = mainCamera.transform.position;

        // Get X distances.
        float xDistance = mainCamera.aspect * mainCamera.orthographicSize;
        float xMax = currentCameraPosition.x + xDistance - boundSize.x;
        float xMin = currentCameraPosition.x - xDistance + boundSize.x;

        // Fix vertical bounds
        if (currentPosition.x < xMin || currentPosition.x > xMax)
        {
            currentPosition.x = Mathf.Clamp(currentPosition.x, xMin, xMax);
        }

        // Get Y distances.
        float yDistance = mainCamera.orthographicSize;
        float yMax = currentCameraPosition.y + yDistance;
        float yMin = currentCameraPosition.y - yDistance;

        // Fix vertical bounds
        if (currentPosition.y < yMin || currentPosition.y > yMax)
        {
            currentPosition.y = Mathf.Clamp(currentPosition.y, yMin, yMax);
        }

        // Set position.
        transform.position = currentPosition;
    }
}