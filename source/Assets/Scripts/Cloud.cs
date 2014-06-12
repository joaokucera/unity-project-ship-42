using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {

	public float speed;

	void Update()
	{
		if (renderer.bounds.max.x < Camera.main.ScreenToWorldPoint(Vector3.zero).x)
		{
			gameObject.SetActive(false);
		}

		transform.Translate(-speed * Time.deltaTime, 0, 0);
	}
}