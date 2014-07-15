﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipHealth : MonoBehaviour
{
    private Dictionary<int, Sprite> energyBarDictionary = new Dictionary<int, Sprite>();
    [SerializeField]
    private SpriteRenderer energySpriteRenderer;
    [SerializeField]
    private int damage = 20;
    [SerializeField]
    private int health = 100;
    [SerializeField]
    private List<Sprite> energyBarSprites;

    void Start()
    {
        if (energySpriteRenderer == null)
        {
            Debug.LogError("There is no energy bar renderer available!");
        }

        if (energyBarSprites == null || energyBarSprites.Count <= 0)
        {
            Debug.LogError("There are no energy bar sprites available!");
        }

        int key = 0;
        foreach (Sprite sprite in energyBarSprites)
        {
            energyBarDictionary.Add(key, sprite);
            key += damage;
        }

        SetEnergyBarSprite();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if ((collider.tag == "Bomb" || collider.tag == "Barril") && collider.renderer.enabled)
        {
            collider.gameObject.SetActive(false);

            health -= damage;
            SetEnergyBarSprite();
        }
    }

    private void SetEnergyBarSprite()
    {
        if (energyBarDictionary.ContainsKey(health))
        {
            energySpriteRenderer.sprite = energyBarDictionary[health];
        }
    }
}