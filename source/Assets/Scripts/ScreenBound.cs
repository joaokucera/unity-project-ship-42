using UnityEngine;
using System.Collections;

public class ScreenBound : MonoBehaviour {
	
	void Start () 
	{
		Vector2 scale = Camera.main.ScreenToWorldPoint(new Vector2(Screen.height, Screen.width));

		transform.CreateScreenTrigger(
			"Screen Trigger Left", Camera.main.ScreenToWorldPoint (new Vector2(0, Screen.height / 2)),
			"InvisiblePoint", "InvisiblePoint", scale);
		
		transform.CreateScreenTrigger(
			"Cloud Trigger Right", Camera.main.ScreenToWorldPoint (new Vector2(Screen.width, Screen.height / 2)),
			"InvisiblePoint", "InvisiblePoint", scale);
	}

}