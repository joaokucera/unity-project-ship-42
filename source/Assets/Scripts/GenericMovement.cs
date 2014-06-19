using UnityEngine;
using System.Collections;

public enum MovementSide
{
	NONE = 0,
	LEFT = -1,
	RIGHT = 1
}

public class GenericMovement : MonoBehaviour {

	[SerializeField] protected float speed;
	[SerializeField] public MovementSide side = MovementSide.LEFT;

	protected Camera mainCamera;
	
	void Start()
	{
		Initialize ();
	}
	
	void Update()
	{
		if (side == MovementSide.LEFT)
		{
			LeftMovement ();
		}
		else if (side == MovementSide.RIGHT)
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

		if (!renderer.isVisible && renderer.bounds.max.x < xLimit)
		{
			gameObject.SetActive(false);
		}

		transform.TranslateTo (-speed, 0, 0, Time.deltaTime);
	}

	protected void RightMovement()
	{
		float xLimit = mainCamera.transform.position.x + (mainCamera.aspect * mainCamera.orthographicSize);

		if (!renderer.isVisible && renderer.bounds.min.x > xLimit)
		{
			gameObject.SetActive(false);
		}
		
		transform.TranslateTo (speed, 0, 0, Time.deltaTime);
	}
}
