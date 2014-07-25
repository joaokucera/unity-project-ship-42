using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPooling : GenericPooling {
	
	[SerializeField] private List<EnemySpawn> spawnEnemyPoints;
	public float spawnTime = 5f;

    void Start()
	{
		if (spawnEnemyPoints == null || spawnEnemyPoints.Count == 0)
		{
			Debug.LogError("There are no spawn points available!");
		}

        base.Initialize();
	}

	void Update () 
	{
		Invoke ("SpawnEnemy", spawnTime);
	}
	
	private void SpawnEnemy ()
	{
		int index = Random.Range (0, spawnEnemyPoints.Count);

		var enemySpawn = spawnEnemyPoints [index];
		enemySpawn.ReverseTranslate ();

		SpawnEnemyFromPool (enemySpawn.transform.position, enemySpawn.side);
		
		CancelInvoke ("SpawnEnemy");
	}

	private void SpawnEnemyFromPool (Vector2 position, MovementSide side)
	{
		GameObject enemy = GetObjectFromPool (position);

		if (enemy != null)
		{
			if (side == MovementSide.LEFTorDOWN)
			{
				enemy.GetComponent<Enemy> ().side = MovementSide.RIGHTorUP;
				enemy.transform.localScale = new Vector2(-1, 1);
			}
			else if (side == MovementSide.RIGHTorUP)
			{
				enemy.GetComponent<Enemy> ().side = MovementSide.LEFTorDOWN;
				enemy.transform.localScale = new Vector2(1, 1);
			}

			enemy.renderer.sortingLayerName = "Foreground";
			enemy.renderer.sortingOrder = 1;
		}
	}
}