using UnityEngine;
using System.Collections;

public class EnemyPooling : GenericPooling {

	private static EnemyPooling Instance;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
	}

	public static void SpawnEnemyFromPool (Vector2 position, MovementSide side)
	{
		GameObject enemy = EnemyPooling.Instance.GetObjectFromPool (position);

		if (enemy != null)
		{
			if (side == MovementSide.LEFT)
			{
				enemy.GetComponent<Enemy> ().side = MovementSide.RIGHT;
				enemy.transform.localScale = new Vector2(-1, 1);
			}
			else if (side == MovementSide.RIGHT)
			{
				enemy.GetComponent<Enemy> ().side = MovementSide.LEFT;
				enemy.transform.localScale = new Vector2(1, 1);
			}

			enemy.renderer.sortingLayerName = "Foreground";
			enemy.renderer.sortingOrder = 2;
		}
	}
}