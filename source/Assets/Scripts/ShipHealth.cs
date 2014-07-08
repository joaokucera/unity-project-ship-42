using UnityEngine;
using System.Collections;

public class ShipHealth : MonoBehaviour {

	[SerializeField] public int health = 100;

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag == "Bomb")
		{
			collider.gameObject.SetActive(false);

			health -= 20;
		}
	}

	void OnGUI()
	{
		GUI.Label (new Rect(20, 20, 200, 100), string.Format("HP: {0}", health));
	}
}