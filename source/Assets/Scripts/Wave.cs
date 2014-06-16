using UnityEngine;
using System;
using System.Collections;

public class Wave : MonoBehaviour {

	public float speed;

	void Update()
	{
		transform.TranslateTo (-speed, 0, 0, Time.deltaTime);
	}
}