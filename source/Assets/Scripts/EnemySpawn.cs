using UnityEngine;
using System.Collections;

public class EnemySpawn : GenericSpawn {
	
	void Start () 
	{
		Vector2 startPosition = StartPosition ();

		transform.parent.CreateTrigger(
			string.Format("{0} Enemy Trigger Up", side), new Vector2(startPosition.x, startPosition.y - yOffset),
			tagName.ToString(), layerName.ToString());
		
		transform.parent.CreateTrigger(
			string.Format("{0} Enemy Trigger Down", side), new Vector2(startPosition.x, 0), 
			tagName.ToString(), layerName.ToString());
		
		transform.position = new Vector2 (startPosition.x, startPosition.y / 2);
	}
	
	void Update () 
	{
		transform.TranslateTo (0, yTranslate, 0, Time.deltaTime);
	}
}