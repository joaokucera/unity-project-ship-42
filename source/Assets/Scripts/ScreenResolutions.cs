using UnityEngine;
using System.Collections;

public class ScreenResolutions : MonoBehaviour {
	
	void Awake () 
	{
		print (Camera.main.aspect);

		// 4:3
		if (Camera.main.aspect > 1.3f && Camera.main.aspect < 1.4f)
		{
			Camera.main.orthographicSize = 15.36f;
		}
		// 3:2
		else if (Camera.main.aspect == 1.5f)
		{
			Camera.main.orthographicSize = 13.66f;
		}
		// 16:9
		else if (Camera.main.aspect == 16/9)
		{
			Camera.main.orthographicSize = 11.53f;
		}
		// 
		else if (Camera.main.aspect == 16/10)
		{
			Camera.main.orthographicSize = 12.81f;
		}
		if (Camera.main.aspect == 17/10)
		{
			Camera.main.orthographicSize = 12.05f;
		}

		// 4.3 = 1.333333
		// 3.2 = 1.5
		// 16.9 = 1.777778
		// 16.10 = 1.6
		// 17.10 = 1.7
	}
}