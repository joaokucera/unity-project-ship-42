using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CloudSize
{
    Big,
    Normal,
    Little
}

public class CloudSpawn : GenericSpawn
{
    [SerializeField]
    private float probability;
    [SerializeField]
    private CloudSize cloudSize;

    void Start()
    {
        Vector2 startPosition = StartPosition();

        transform.parent.CreateTrigger(
            string.Format("Clouds Trigger Up ({0})", cloudSize), new Vector2(startPosition.x, startPosition.y * 2 - 1),
            tagName.ToString(), layerName.ToString());

        transform.parent.CreateTrigger(
            string.Format("Clouds Trigger Down ({0})", cloudSize), new Vector2(startPosition.x, yOffset),
            tagName.ToString(), layerName.ToString());

        transform.position = new Vector2(startPosition.x, startPosition.y - yOffset);

        InvokeRepeating("SpawnEvaluate", 2f, 2f);
    }

    private void SpawnEvaluate()
    {
        float value = Random.value * 100;

        if (value <= probability)
        {
            SpawnCloud();
        }
    }

    private void SpawnCloud()
    {
        ReverseTranslate();

        CloudPooling.Instance.SpawnCloudFromPool(transform.position, cloudSize);
    }
}
