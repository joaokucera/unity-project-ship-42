using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
{
    private Camera mainCamera;
    private float xOffset = 5f;

    [SerializeField]
    private Modal modalScript = null;
    [SerializeField]
    private SpriteRenderer playButton = null, reloadButton = null;

    void Start()
    {
        playButton.enabled = false;
        reloadButton.enabled = false;

        mainCamera = Camera.main;

        transform.position = new Vector2(mainCamera.aspect * mainCamera.orthographicSize - renderer.bounds.size.x,
                                         mainCamera.orthographicSize - renderer.bounds.size.y);
        playButton.transform.position = new Vector2(mainCamera.aspect * mainCamera.orthographicSize - playButton.bounds.size.x - xOffset,
                                                    mainCamera.orthographicSize - playButton.bounds.size.y);
        reloadButton.transform.position = new Vector2(mainCamera.aspect * mainCamera.orthographicSize - reloadButton.bounds.size.x,
                                                      mainCamera.orthographicSize - reloadButton.bounds.size.y);
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

    private void CheckAction(Vector2 position)
    {
        if (position.HasActivated(transform.position, renderer.bounds.size, false, true))
        {
            PauseOrReload();
        }
        else if (position.HasActivated(playButton.transform.position, playButton.bounds.size, false, true))
        {
            Continue();
        }
    }

    private void PauseOrReload()
    {
        if (Time.timeScale == 1f)
        {
            ShowContinueAndReloadButtons();

            if (modalScript.OnVisible())
            {
                Time.timeScale = 0f;
            }
        }
        else
        {
            Time.timeScale = 1f;
            Application.LoadLevel("Level");
        }
    }

    private void Continue()
    {
        HideContinueAndReloadButtons();

        if (modalScript.OnInvisible())
        {
            Time.timeScale = 1f;
        }
    }

    public void HideContinueAndReloadButtons()
    {
        renderer.enabled = true;
        playButton.enabled = false;
        reloadButton.enabled = false;
    }

    public void ShowContinueAndReloadButtons()
    {
        renderer.enabled = false;
        playButton.enabled = true;
        reloadButton.enabled = true;
    }
}
