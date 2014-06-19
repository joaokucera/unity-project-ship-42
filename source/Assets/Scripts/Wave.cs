using UnityEngine;
using System;
using System.Collections;

public class Wave : GenericMovement {
	
	void Update()
	{
		int direction = (int)side;

		transform.TranslateTo (direction * speed, 0, 0, Time.deltaTime);
	}
}