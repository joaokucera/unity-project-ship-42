using UnityEngine;
using System.Collections;

public class Modal : MonoBehaviour
{
    [SerializeField]
    private Pause pauseScript = null;
    [SerializeField]
    private Menu menuScript = null;

    [SerializeField]
    private Animator playButtonAnimator = null;
    [SerializeField]
    private Animator giftBoxAnimator = null;

    [SerializeField]
    private Transform lightBox = null, background = null;

    [SerializeField]
    private SpriteRenderer closeButton = null, musicButton = null, fxsButton = null, acceleratorButton = null, hapticsButton = null;
    [SerializeField]
    private Sprite buttonON = null, buttonOFF = null;

    private Camera mainCamera;

    void Start()
    {
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
        Vector2 position = Vector2.zero;

#if UNITY_EDITOR
        if (Controls.MouseAction(ref position))
        {
#else
        if (Controls.TouchAction(ref position))
        {
#endif
            CheckAction(position);
        }
    }

    public bool OnVisible()
    {
        gameObject.SetActive(true);

        if (menuScript != null)
        {
            playButtonAnimator.enabled = false;
            giftBoxAnimator.enabled = false;
        }

        if (pauseScript != null)
        {
            pauseScript.ShowContinueAndReloadButtons();

            Time.timeScale = 0f;
        }

        return true;
    }

    public bool OnInvisible()
    {
        gameObject.SetActive(false);

        if (menuScript != null)
        {
            playButtonAnimator.enabled = true;
            giftBoxAnimator.enabled = true;

            menuScript.ReactiveButtons();
        }

        if (pauseScript != null)
        {
            pauseScript.HideContinueAndReloadButtons();

            Time.timeScale = 1f;
        }

        return true;
    }

    private void CheckAction(Vector2 position)
    {
        if (position.HasActivated(closeButton.transform.position, closeButton.bounds.size, false, true))
        {
            OnInvisible();
        }
        else if (position.HasActivated(musicButton.transform.position, musicButton.bounds.size))
        {
            ChangeButtonSprite(musicButton);
        }
        else if (position.HasActivated(fxsButton.transform.position, fxsButton.bounds.size))
        {
            ChangeButtonSprite(fxsButton);
        }
        else if (position.HasActivated(acceleratorButton.transform.position, acceleratorButton.bounds.size))
        {
            ChangeButtonSprite(acceleratorButton);
        }
        else if (position.HasActivated(hapticsButton.transform.position, hapticsButton.bounds.size))
        {
            ChangeButtonSprite(hapticsButton);
        }
    }

    //private bool HasActivated(Vector2 positionA, Vector2 positionB, Vector2 size)
    //{
    //    bool hasActivated = Mathf.Abs(positionA.x - positionB.x) <= size.x &&
    //                        Mathf.Abs(positionA.y - positionB.y) <= size.y;

    //    if (hasActivated)
    //    {
    //        SoundEffectScript.Instance.PlaySound(SoundEffectClip.ClickButton);
    //    }

    //    return hasActivated;
    //}

    private void ChangeButtonSprite(SpriteRenderer buttonRenderer)
    {
        SoundEffectScript.Instance.PlaySound(SoundEffectClip.ClickButton);

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
