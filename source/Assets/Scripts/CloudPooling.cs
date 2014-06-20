using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudPooling : GenericPooling {

	private static CloudPooling Instance;

	[SerializeField] private List<Sprite> cloudSprites;
	
	void Awake()
	{
		Instance = this;
	
		if (cloudSprites == null || cloudSprites.Count == 0) 
		{
			Debug.LogError("There are no cloud sprites available!");
		}
	}
	
	public static void SpawnCloudFromPool (Vector2 position)
	{
		GameObject cloud = CloudPooling.Instance.GetObjectFromPool (position);

		if (cloud != null)
		{
			cloud.renderer.sortingLayerName = "Background";
			cloud.renderer.sortingOrder = 1;
			
			int index = Random.Range (0, CloudPooling.Instance.cloudSprites.Count);
			((SpriteRenderer)cloud.renderer).sprite = CloudPooling.Instance.cloudSprites [index];
		}
	}
}