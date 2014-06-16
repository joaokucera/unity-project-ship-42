using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundPosition : MonoBehaviour {

	[SerializeField] private Transform leftSky;
	[SerializeField] private Transform rightSky;

	void Awake()
	{
		leftSky.renderer.sortingLayerName = "Background";
		rightSky.renderer.sortingLayerName = "Background";

		leftSky.renderer.sortingOrder = 0;
		rightSky.renderer.sortingOrder = 0;

		Vector2 leftSkyPosition = leftSky.position;
		Vector2 rightSkyPosition = rightSky.position;

		leftSkyPosition.x = -(leftSky.renderer.bounds.size / 2).x;
		rightSkyPosition.x = (rightSky.renderer.bounds.size / 2).x;

		leftSky.position = leftSkyPosition;
		rightSky.position = rightSkyPosition;
	} 
}