using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public enum WaveGroup
{
	First,
	Second,
	Third
}

public class WaveParallax : MonoBehaviour {

	[SerializeField] private WaveGroup waveGroup;

	private List<Transform> waves = new List<Transform>();
	private Camera mainCamera;

	void Start()
	{
		mainCamera = Camera.main;

		for (int i = 0; i < transform.childCount; i++) 
		{
			waves.Add(transform.GetChild(i));
		}
		
		if (waves == null || waves.Count == 0) 
		{
			Debug.LogError("There are no waves available!");
		}

		waves.OrderBy (w => w.position.x).ToList();

		switch (waveGroup) {
			case WaveGroup.First:
				waves.ForEach(w => {
					w.renderer.sortingLayerName = "Background";
					w.renderer.sortingOrder = 1;
				});
				break;
			case WaveGroup.Second:
				waves.ForEach(w => {
					w.renderer.sortingLayerName = "Foreground";
					w.renderer.sortingOrder = 0;
				});
				break;
			case WaveGroup.Third:
				waves.ForEach(w => {
					w.renderer.sortingLayerName = "Foreground";
					w.renderer.sortingOrder = 1;
				});
				break;
		}
	}

	void Update () 
	{
		Transform firstWave = waves.FirstOrDefault ();

		if (firstWave != null)
		{
			float xMin = mainCamera.transform.position.x - (mainCamera.aspect * mainCamera.orthographicSize);

			if (!firstWave.renderer.isVisible && firstWave.renderer.bounds.max.x < xMin)
			{
				Transform lastWave = waves.LastOrDefault();

				//print("Name: " + firstWave.name + " | " + lastWave.name);

				Vector3 lastPosition = lastWave.position;
				Vector3 lastSize = lastWave.renderer.bounds.max - lastWave.renderer.bounds.min;

				//print("POS: " + lastPosition.x + " | " + lastSize.x);

				firstWave.position = new Vector3(lastPosition.x + lastSize.x, 
				                                  firstWave.position.y, 
				                                  firstWave.position.z);
				
				waves.Remove(firstWave);
				waves.Add(firstWave);
			}
		}
	}
}
