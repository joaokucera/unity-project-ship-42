using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour {

	private float speed;
	private MovementSide movementSide = MovementSide.NONE;
	private Camera mainCamera;
	private Vector2 boundSize;
	private float fixedVerticalPosition;

	void Start () 
	{
		mainCamera = Camera.main;
		boundSize = renderer.bounds.size / 2;

		float yPosition = -mainCamera.orthographicSize + (renderer.bounds.size.y / 3f);
		Vector2 startPosition = new Vector2 (0, yPosition);

		transform.position = startPosition;
		fixedVerticalPosition = startPosition.y;
	}

	void Update () 
	{
        speed = CrewStatus.Instance.captainStamina / 10;

		transform.TranslateTo ((int)movementSide * speed, 0, 0, Time.deltaTime);

		transform.position = new Vector2 (transform.position.x, fixedVerticalPosition);

#if UNITY_EDITOR
		MouseAction();
#else
		TouchAction ();
#endif

		// Enforce ship inside the screen.
		EnforceBounds ();
	}

	private void MouseAction ()
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

	private void TouchAction ()
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
	private void EnforceBounds ()
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