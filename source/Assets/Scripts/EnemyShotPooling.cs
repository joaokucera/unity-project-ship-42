using UnityEngine;
using System.Collections;

public class EnemyShotPooling : GenericPooling {

	public static EnemyShotPooling Instance;

	void Start()
	{
		if (Instance == null)
		{
			Instance = this;
		}

        base.Initialize();
	}
	
	public void SpawnShotFromPool (Vector2 position)
	{
		GameObject shot = GetObjectFromPool (position);
		
		if (shot != null)
		{
			shot.renderer.sortingLayerName = "Foreground";
			shot.renderer.sortingOrder = 0;
		}
	}
}
