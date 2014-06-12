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

	private SpriteRenderer spriteRenderer;
	private MovementSide movementSide = MovementSide.NONE;
	private bool enableLeftButton = true;
	private bool enableRightButton = true;
	private float yStartPosition;

	void Start () 
	{
		spriteRenderer = (SpriteRenderer)renderer;

		Vector2 startPosition = Camera.main.ScreenToWorldPoint (
			new Vector2 (Screen.width / 2, spriteRenderer.bounds.size.x * 3.5f));

		transform.position = startPosition;

		yStartPosition = startPosition.y;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag == "InvisiblePoint")
		{
			movementSide = MovementSide.NONE;

			if (collider.name.Contains("Left"))
			{
				enableLeftButton = false;
			}
			else if (collider.name.Contains("Right"))
			{
				enableRightButton = false;
			}
		}
	}

	void Update () 
	{
		transform.TranslateTo ((int)movementSide * speed, 0, 0, Time.deltaTime);

		transform.position = new Vector2 (transform.position.x, yStartPosition);
	}

	void OnGUI()
	{
		if (enableLeftButton && GUI.Button(new Rect(0, Screen.height - 40, 40, 40), "<<"))
		{
			movementSide = MovementSide.LEFT;

			enableRightButton = true;
		}
		else if (enableRightButton && GUI.Button(new Rect(Screen.width - 40, Screen.height - 40, 40, 40), ">>"))
		{
			movementSide = MovementSide.RIGHT;

			enableLeftButton = true;
		}
	}
}