using UnityEngine;
using System;
using System.Collections;

public enum WaveStartPosition
{
	PointZero,
	AfterScreen,
	LastOne
}

public enum WaveType
{
	Wave01 = 50,
	Wave02 = 25,
	Wave03 = 0,
}

public class Wave : MonoBehaviour {

	public float speed;
	public WaveStartPosition waveStartPosition = WaveStartPosition.PointZero;

	void Start () 
	{
		Vector2 startPosition = Vector2.zero;

		if (waveStartPosition == WaveStartPosition.PointZero)
		{
			startPosition = Camera.main.ScreenToWorldPoint (Vector2.zero);
		}
		else if (waveStartPosition == WaveStartPosition.AfterScreen)
		{
			startPosition = Camera.main.ScreenToWorldPoint (new Vector2(Screen.width, 0));
		}
		else
		{
			startPosition = Camera.main.ScreenToWorldPoint (new Vector2(Screen.width * 2, 0));
		}

		WaveType waveType = (WaveType)Enum.Parse(typeof(WaveType), ((SpriteRenderer)renderer).sprite.name, true);
		float yPosition = ((int)waveType / 100f);

		transform.position = new Vector2(startPosition.x, startPosition.y + yPosition);
	}

	void Update()
	{
		transform.TranslateTo (-speed, 0, 0, Time.deltaTime);
	}
}