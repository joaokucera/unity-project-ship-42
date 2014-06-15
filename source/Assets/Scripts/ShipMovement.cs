using UnityEngine;
using System.Collections;

public enum MovementSide
{
	NONE = 0,
	LEFT = -1,
	RIGHT = 1
}

public class ShipMovement : MonoBehaviour {

	public float speed;

	private MovementSide movementSide = MovementSide.NONE;
	private Camera mainCamera;
	private SpriteRenderer spriteRenderer;
	private Vector2 boundSize;

	private float verticalFixedPosition;

	void Start () 
	{
		mainCamera = Camera.main;
		spriteRenderer = (SpriteRenderer)renderer;
		boundSize = spriteRenderer.sprite.bounds.size / 2;


		Vector2 startPosition = //new Vector2((mainCamera.aspect) / 2,
//		                                    0); 
//		Debug.Log (startPosition);

		Camera.main.ScreenToWorldPoint (new Vector2 (Screen.width / 2, spriteRenderer.bounds.size.x * 3.5f));

		transform.position = startPosition;
		verticalFixedPosition = startPosition.y;
	}

	void Update () 
	{
		transform.TranslateTo ((int)movementSide * speed, 0, 0, Time.deltaTime);

		transform.position = new Vector2 (transform.position.x, verticalFixedPosition);

		// Enforce ship inside the screen.
		EnforceBounds ();
	}

	void OnGUI()
	{
		if (GUI.Button(new Rect(0, Screen.height - 40, 40, 40), "<<"))
		{
			movementSide = MovementSide.LEFT;
		}
		else if (GUI.Button(new Rect(Screen.width - 40, Screen.height - 40, 40, 40), ">>"))
		{
			movementSide = MovementSide.RIGHT;
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