using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenericScreen : MonoBehaviour {

	[SerializeField] private Transform title;
	[SerializeField] private SpriteRenderer giftBoxRenderer;
	[SerializeField] private List<Sprite> giftBoxSprites;
	
	protected Camera mainCamera;
	
	void Start () 
	{
		Initialize ();
	}

	protected void Initialize ()
	{
		mainCamera = Camera.main;

		title.position = new Vector2 ((mainCamera.aspect * mainCamera.orthographicSize / 2) - title.renderer.bounds.size.x / 2, 
		                              (mainCamera.orthographicSize + title.renderer.bounds.size.y) / 2);

		if (giftBoxSprites == null || giftBoxSprites.Count == 0) 
		{
			Debug.LogError ("There are no gift box sprites available!");
		}

		int index = Random.Range (0, giftBoxSprites.Count);
		giftBoxRenderer.sprite = giftBoxSprites [index];
	}
}
