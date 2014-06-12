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

		float width = Screen.width * 1.1f;

		transform.parent.CreateTrigger(
			"Cloud Trigger Up", Camera.main.ScreenToWorldPoint (new Vector2(width, Screen.height)),
			"InvisiblePoint", "InvisiblePoint");

		transform.parent.CreateTrigger(
			"Cloud Trigger Down", Camera.main.ScreenToWorldPoint (new Vector2(width, Screen.height * 75 / 100)),
			"InvisiblePoint", "InvisiblePoint");

		transform.position = Camera.main.ScreenToWorldPoint (new Vector2(width, Screen.height * 87.5f / 100));
	
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

		int index = Random.Range (0, cloudSprites.Count);
		((SpriteRenderer)cloud.renderer).sprite = cloudSprites [index];
	}

	private void ReverseTranslate ()
	{
		yTranslate *= -1;
	}
}