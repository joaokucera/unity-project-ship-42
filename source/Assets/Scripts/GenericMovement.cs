using UnityEngine;
using System.Collections;

public enum MovementSide
{
	NONE = 0,
	LEFTorDOWN = -1,
	RIGHTorUP = 1,
}

public enum MovementAngle
{
	HORIZONTAL,
	VERTICAL
}

public class GenericMovement : MonoBehaviour {

	[SerializeField] protected float horizontalSpeed;
	[SerializeField] protected float verticalSpeed;
	[SerializeField] public MovementSide side = MovementSide.LEFTorDOWN;
	[SerializeField] public MovementAngle angle = MovementAngle.HORIZONTAL;

	protected Camera mainCamera;
	
	void Start()
	{
		Initialize ();
	}
	
	void Update()
	{
		if (side == MovementSide.LEFTorDOWN)
		{
			LeftMovement ();
		}
		else if (side == MovementSide.RIGHTorUP)
		{
			RightMovement ();
		}
	}

	protected void Initialize()
	{
		mainCamera = Camera.main;
	}
	
	protected void LeftMovement()
	{
		float xLimit = mainCamera.transform.position.x - (mainCamera.aspect * mainCamera.orthographicSize);
		float yLimit = mainCamera.transform.position.y - mainCamera.orthographicSize;

		if (!renderer.isVisible && (renderer.bounds.max.x < xLimit || renderer.bounds.max.y < yLimit))
		{
			gameObject.SetActive(false);
		}

		transform.TranslateTo (-horizontalSpeed, -verticalSpeed, 0, Time.deltaTime);
	}

	protected void RightMovement()
	{
		float xLimit = mainCamera.transform.position.x + (mainCamera.aspect * mainCamera.orthographicSize);
		float yLimit = mainCamera.transform.position.y + mainCamera.orthographicSize;

		if (!renderer.isVisible && (renderer.bounds.min.x > xLimit || renderer.bounds.min.y > yLimit))
		{
			gameObject.SetActive(false);
		}
		
		transform.TranslateTo (horizontalSpeed, verticalSpeed, 0, Time.deltaTime);
	}
}
