using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Credits : GenericScreen {
	
	void Update()
	{
#if UNITY_EDITOR
			MouseAction();
#else
			TouchAction ();
#endif
	}
	
	private void MouseAction ()
	{
		// Just 1 tap.
		if (Input.GetButtonDown("Fire1"))
		{
			Application.LoadLevel (SceneName.Menu.ToString());
		}
	}
	
	private void TouchAction ()
	{
		// Just 1 tap.
		if (Input.touchCount == 1)
		{
			Touch touch = Input.GetTouch(0);
			
			if (touch.phase == TouchPhase.Began)
			{
				Application.LoadLevel (SceneName.Menu.ToString());
			}
		}
	}
}