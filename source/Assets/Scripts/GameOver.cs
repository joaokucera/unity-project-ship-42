using UnityEngine;
using System.Collections;
using System;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private Transform lightBox = null;

    [SerializeField]
    private GameObject playerShotSpawnerObject = null, crewStatusObject = null, pauseObject = null, shipStatus = null;

    [SerializeField]
    private GUIText scoreText = null, lastScoreText = null, highScoreText = null;

    [SerializeField]
    private SpriteRenderer reloadButton = null, endGameButton = null;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        Vector2 modalScale = lightBox.localScale;
        modalScale.x = mainCamera.aspect * mainCamera.orthographicSize / 2;
        modalScale.y = mainCamera.aspect * mainCamera.orthographicSize / 2;
        lightBox.localScale = modalScale;
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

    public void OnVisible()
    {
        SoundEffectScript.Instance.PlaySound(SoundEffectClip.GameOver);

        gameObject.SetActive(true);

        playerShotSpawnerObject.SetActive(false);
        crewStatusObject.SetActive(false);
        pauseObject.SetActive(false);
        shipStatus.SetActive(false);

        int sailedTime = (int)GameSettings.Instance.sailedTime;
        TimeSpan time = new TimeSpan(0, 0, sailedTime);

        scoreText.text = string.Format(scoreText.text, time.Minutes.ToString("00"), time.Seconds.ToString("00"));
        lastScoreText.text = string.Format(scoreText.text, time.Minutes.ToString("00"), time.Seconds.ToString("00"));
        highScoreText.text = string.Format(scoreText.text, time.Minutes.ToString("00"), time.Seconds.ToString("00"));

        Time.timeScale = 0f;
    }

    public void CheckAction(Vector2 position)
    {
        if (position.HasActivated(reloadButton.transform.position, reloadButton.bounds.size, false, true))
        {
            ApplyButton(SceneName.Level);
        }
        else if (position.HasActivated(endGameButton.transform.position, endGameButton.bounds.size, false, true))
        {
            ApplyButton(SceneName.Menu);
        }
    }

    private void ApplyButton(SceneName sceneName)
    {
        AdvertisementManager.Instance.ShowAds(sceneName);

        gameObject.SetActive(false);
    }
}
