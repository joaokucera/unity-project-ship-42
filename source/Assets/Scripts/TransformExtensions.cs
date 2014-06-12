using UnityEngine;
using System.Collections;

public static class TransformExtensions {
	
	public static void TranslateTo(this Transform transform, Vector3 translation, float speed)
	{
		transform.Translate (translation * speed);
	}

	public static void TranslateTo(this Transform transform, float x, float y, float z, float speed)
	{
		transform.TranslateTo(new Vector3(x, y, z), speed);
	}

	public static GameObject CreateTrigger(this Transform transform, string name, Vector2 position, string tag, string layerName)
	{
		GameObject trigger = new GameObject (name);
		trigger.AddComponent<CircleCollider2D> ().isTrigger = true;
		trigger.AddComponent<Rigidbody2D> ().gravityScale = 0;
		trigger.transform.position = position;
		trigger.tag = tag;
		trigger.layer = LayerMask.NameToLayer (layerName);
		trigger.transform.parent = transform;

		return trigger;
	}

	public static GameObject CreateScreenTrigger(this Transform transform, string name, Vector2 position, 
	                                       		 string tag, string layerName, Vector2 scale)
	{
		GameObject trigger = new GameObject (name);
		trigger.AddComponent<BoxCollider2D> ().isTrigger = true;
		trigger.AddComponent<Rigidbody2D> ().gravityScale = 0;
		trigger.transform.position = position;
		trigger.tag = tag;
		trigger.layer = LayerMask.NameToLayer (layerName);
		trigger.transform.parent = transform;
		trigger.transform.localScale = scale;

		return trigger;
	}
}
