using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TagName
{
    CloudsSpawnerLimit,
    EnemiesSpawnerLimit,
    Barril,
    EnemyAmmo,
    FriendsAirplanesSpawnerLimit,
    SafeBuoyLimit
}

public enum LayerName
{
    CloudsSpawnerLimit,
    EnemiesSpawnerLimit,
    FriendsAirplanesSpawnerLimit,
    SafeBuoyLimit
}

public class GenericPooling : MonoBehaviour
{
    [SerializeField]
    protected GameObject prefab;
    [SerializeField]
    private int poolSize;
    [SerializeField]
    private bool poolCanGrow;

    private List<GameObject> pool = new List<GameObject>();

    void Start()
    {
        Initialize();
    }

    public GameObject GetObjectFromPool(Vector2 position, bool active = true)
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                return PrepareObjectToResponse(obj, position, active);
            }
        }

        if (poolCanGrow)
        {
            GameObject obj = CreateNewObject();

            return PrepareObjectToResponse(obj, position, active);
        }

        return null;
    }

    protected void Initialize()
    {
        if (prefab == null)
        {
            Debug.LogError("Has not been defined a prefab!");
        }

        for (int i = 0; i < poolSize; i++)
        {
            CreateNewObject();
        }
    }

    private GameObject CreateNewObject()
    {
        GameObject newObject = Instantiate(prefab) as GameObject;
        newObject.SetActive(false);

        pool.Add(newObject);

        return newObject;
    }

    private GameObject PrepareObjectToResponse(GameObject obj, Vector2 position, bool active)
    {
        obj.transform.position = position;
        obj.SetActive(active);

        return obj;
    }
}
