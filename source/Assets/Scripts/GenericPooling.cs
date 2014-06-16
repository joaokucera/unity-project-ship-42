using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenericPooling : MonoBehaviour {

	public GameObject prefab;
	public int poolSize;
	public bool poolCanGrow;

	protected List<GameObject> pool = new List<GameObject>();
	
	void Start () 
	{
		Initialize ();
	}

	public GameObject GetObjectFromPool(Vector2 position, bool active = true)
	{
		foreach (GameObject obj in pool) 
		{
			if (!obj.activeInHierarchy)
			{
				return PrepareObjectToResponse (obj, position, active);
			}
		}

		if (poolCanGrow)
		{
			GameObject obj = CreateNewObject();

			return PrepareObjectToResponse(obj, position, active);
		}

		return null;
	}

	protected void Initialize ()
	{
		if (prefab == null) {
			Debug.LogError ("Não foi definido um prefab!");
		}

		for (int i = 0; i < poolSize; i++) {
			CreateNewObject ();
		}
	}

	private GameObject CreateNewObject ()
	{
		GameObject newObject = (GameObject)Instantiate (prefab);
		newObject.SetActive (false);

		pool.Add (newObject);

		return newObject;
	}

	private GameObject PrepareObjectToResponse (GameObject obj, Vector2 position, bool active)
	{
		obj.transform.position = position;
		obj.SetActive (active);

		return obj;
	}
}
