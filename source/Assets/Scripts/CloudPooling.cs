using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudPooling : GenericPooling {

	public List<Sprite> cloudSprites;
	public float probability;

	private float yTranslate = 1;

	void Start () 
	{
		base.Initialize ();

		Camera mainCamera = Camera.main;
		Vector2 currentCameraPosition = mainCamera.transform.position;

		float xPosition = currentCameraPosition.x + (mainCamera.aspect * mainCamera.orthographicSize);
		float yPosition = currentCameraPosition.x + mainCamera.orthographicSize;

		transform.parent.CreateTrigger(
			"Cloud Trigger Up", new Vector2(xPosition, yPosition),
			"InvisiblePoint", "InvisiblePoint");

		transform.parent.CreateTrigger(
			"Cloud Trigger Down", new Vector2(xPosition, 0), 
			"InvisiblePoint", "InvisiblePoint");

		transform.position = new Vector2 (xPosition, yPosition / 2);
	
		if (cloudSprites == null || cloudSprites.Count == 0) 
		{
			Debug.LogError("Não foram inseridos os sprites das nuvens!");
		}

		InvokeRepeating ("SpawnEvaluate", 2f, 2f);
	}

	void Update () 
	{
		transform.TranslateTo (0, yTranslate, 0, Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag == "InvisiblePoint")
		{
			ReverseTranslate();
		}
	}

	private void SpawnEvaluate()
	{
		float value = Random.value * 100;

		if (value <= probability)
		{
			SpawnCloud ();
		}
	}

	private void SpawnCloud ()
	{
		ReverseTranslate ();

		GameObject cloud = GetObjectFromPool (transform.position);
		cloud.renderer.sortingLayerName = "Background";
		cloud.renderer.sortingOrder = 1;

		int index = Random.Range (0, cloudSprites.Count);
		((SpriteRenderer)cloud.renderer).sprite = cloudSprites [index];
	}

	private void ReverseTranslate ()
	{
		yTranslate *= -1;
	}
}