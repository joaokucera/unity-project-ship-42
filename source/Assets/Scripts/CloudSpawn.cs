using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudSpawn : GenericSpawn {
	
	[SerializeField] private float probability;
	
	void Start () 
	{
		Vector2 startPosition = StartPosition ();

		transform.parent.CreateTrigger(
			"Cloud Trigger Up", startPosition,
			tagName.ToString(), layerName.ToString());
		
		transform.parent.CreateTrigger(
			"Cloud Trigger Down", new Vector2(startPosition.x, yOffset), 
			tagName.ToString(), layerName.ToString());
		
		transform.position = new Vector2 (startPosition.x, startPosition.y / 2);
		
		InvokeRepeating ("SpawnEvaluate", 2f, 2f);
	}
	
	void Update () 
	{
		transform.TranslateTo (0, yTranslate, 0, Time.deltaTime);
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

		if (CloudPooling.Instance == null)
		{
			Debug.LogError("CloudPooling.Instance == null");
		}

		CloudPooling.Instance.SpawnCloudFromPool (transform.position);
	}
}
