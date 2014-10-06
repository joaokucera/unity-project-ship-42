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
    private Transform lightBox = null;

    [SerializeField]
    private SpriteRenderer closeButton = null, musicButton = null, fxsButton = null, acceleratorButton = null, hapticsButton = null;
    [SerializeField]
    private Sprite buttonON = null, buttonOFF = null;

    [SerializeField]
    private MusicManager musicManagerScript;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        Vector2 modalScale = lightBox.localScale;
        modalScale.x = mainCamera.aspect * mainCamera.orthographicSize / 2;
        modalScale.y = mainCamera.aspect * mainCamera.orthographicSize / 2;
        lightBox.localScale = modalScale;

        SetButtonSettings(musicButton, GameSettings.Instance.musicEnabled);
        SetButtonSettings(fxsButton, GameSettings.Instance.specialEffectsEnabled);
        SetButtonSettings(acceleratorButton, GameSettings.Instance.acceleratorEnabled);
        SetButtonSettings(hapticsButton, GameSettings.Instance.hapticsEnabled);
    }

    void Update()
    {
        Vector2 position = Vector2.zero;

#if UNITY_EDITOR || UNITY_WEBPLAYER
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
        else if (position.HasActivated(musicButton.transform.position, musicButton.bounds.size, false, true))
        {
            ChangeButtonSprite(musicButton, ref GameSettings.Instance.musicEnabled);

            musicManagerScript.PlayOrPause();
        }
        else if (position.HasActivated(fxsButton.transform.position, fxsButton.bounds.size, false, true))
        {
            ChangeButtonSprite(fxsButton, ref GameSettings.Instance.specialEffectsEnabled);
        }
        else if (position.HasActivated(acceleratorButton.transform.position, acceleratorButton.bounds.size, false, true))
        {
            ChangeButtonSprite(acceleratorButton, ref GameSettings.Instance.acceleratorEnabled);
        }
        else if (position.HasActivated(hapticsButton.transform.position, hapticsButton.bounds.size, false, true))
        {
            ChangeButtonSprite(hapticsButton, ref GameSettings.Instance.hapticsEnabled);
        }
    }

    private void ChangeButtonSprite(SpriteRenderer buttonRenderer, ref bool settingsEnabled)
    {
        settingsEnabled = !settingsEnabled;

        SetButtonSettings(buttonRenderer, settingsEnabled);
    }

    private void SetButtonSettings(SpriteRenderer buttonRenderer, bool settingsEnabled)
    {
        if (settingsEnabled)
        {
            buttonRenderer.sprite = buttonON;
        }
        else
        {
            buttonRenderer.sprite = buttonOFF;
        }
    }
}