using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {

	public float speed;

	private Camera mainCamera;

	void Start()
	{
		mainCamera = Camera.main;
	}

	void Update()
	{
		float xMin = mainCamera.transform.position.x - (mainCamera.aspect * mainCamera.orthographicSize);

		if (!renderer.isVisible && renderer.bounds.max.x < xMin)
		{
			gameObject.SetActive(false);
		}

		transform.Translate(-speed * Time.deltaTime, 0, 0);
	}
}