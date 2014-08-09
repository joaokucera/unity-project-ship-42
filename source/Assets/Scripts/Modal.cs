﻿using UnityEngine;
using System.Collections;

public class Modal : MonoBehaviour
{
    [SerializeField]
    private Menu menuScript;

    [SerializeField]
    private Animator playButtonAnimator;
    [SerializeField]
    private Animator giftBoxAnimator;

    [SerializeField]
    private Transform lightBox = null, background = null;

    [SerializeField]
    private SpriteRenderer closeButton = null, musicButton = null, fxsButton = null, acceleratorButton = null, hapticsButton = null;
    [SerializeField]
    private Sprite buttonON = null, buttonOFF = null;

    private Camera mainCamera;

    void Start()
    {
        gameObject.SetActive(false);

        mainCamera = Camera.main;

        Vector2 modalScale = lightBox.localScale;
        modalScale.x = mainCamera.aspect * mainCamera.orthographicSize / 2;
        modalScale.y = mainCamera.aspect * mainCamera.orthographicSize / 2;
        lightBox.localScale = modalScale;

        float size = mainCamera.aspect * (mainCamera.orthographicSize / 20);
        Vector2 backgroundScale = background.localScale;
        backgroundScale.x *= size;
        backgroundScale.y *= size;
        background.localScale = backgroundScale;
    }

    void Update()
    {
#if UNITY_EDITOR
        CheckAction(Controls.MouseAction());
#else
		CheckAction(Controls.TouchAction());
#endif
    }

    public void OnVisible()
    {
        gameObject.SetActive(true);

        playButtonAnimator.enabled = false;
        giftBoxAnimator.enabled = false;
    }

    private void OnInvisible()
    {
        playButtonAnimator.enabled = true;
        giftBoxAnimator.enabled = true;

        gameObject.SetActive(false);

        menuScript.ReactiveButtons();
    }

    private void CheckAction(Vector2 position)
    {
        if (HasActivated(position, closeButton.transform.position, closeButton.bounds.size))
        {
            OnInvisible();
        }
        else if (HasActivated(position, musicButton.transform.position, musicButton.bounds.size))
        {
            ChangeButtonSprite(musicButton);
        }
        else if (HasActivated(position, fxsButton.transform.position, fxsButton.bounds.size))
        {
            ChangeButtonSprite(fxsButton);
        }
        else if (HasActivated(position, acceleratorButton.transform.position, acceleratorButton.bounds.size))
        {
            ChangeButtonSprite(acceleratorButton);
        }
        else if (HasActivated(position, hapticsButton.transform.position, hapticsButton.bounds.size))
        {
            ChangeButtonSprite(hapticsButton);
        }
    }
    
    private bool HasActivated(Vector2 positionA, Vector2 positionB, Vector2 size)
    {
        return Mathf.Abs(positionA.x - positionB.x) <= size.x / 2 &&
               Mathf.Abs(positionA.y - positionB.y) <= size.y / 2;
    }

    private void ChangeButtonSprite(SpriteRenderer buttonRenderer)
    {
        if (buttonRenderer.sprite.name == buttonON.name)
        {
            buttonRenderer.sprite = buttonOFF;
        }
        else
        {
            buttonRenderer.sprite = buttonON;
        }
    }
}
