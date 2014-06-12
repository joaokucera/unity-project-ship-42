using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class WaveParallax : MonoBehaviour {

	public List<Transform> waves;

	void Start()
	{
		if (waves == null || waves.Count == 0) 
		{
			Debug.LogError("Não foram inseridos os transforms das ondas!");
		}

		waves.OrderBy (w => w.position.x).ToList();
	}

	void Update () 
	{
		Transform firstWave = waves.FirstOrDefault ();

		if (firstWave != null)
		{
			if (firstWave.renderer.bounds.max.x < Camera.main.ScreenToWorldPoint(Vector3.zero).x)
			{
				Transform lastWave = waves.LastOrDefault();

				Vector3 lastPosition = lastWave.position;
				Vector3 lastSize = lastWave.renderer.bounds.max - lastWave.renderer.bounds.min;
				
				firstWave.position = new Vector3(lastPosition.x + lastSize.x, 
				                                  firstWave.position.y, 
				                                  firstWave.position.z);
				
				waves.Remove(firstWave);
				waves.Add(firstWave);
			}
		}
	}
}
