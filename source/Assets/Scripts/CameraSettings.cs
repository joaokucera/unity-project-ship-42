using UnityEngine;
using System.Collections;

public class CameraSettings : MonoBehaviour {

	private const float OrthographicSize = 15.4f;

	void Awake () 
	{
		Camera mainCamera = Camera.main;

		mainCamera.orthographic = true;
		mainCamera.orthographicSize = CameraSettings.OrthographicSize;
	}
}