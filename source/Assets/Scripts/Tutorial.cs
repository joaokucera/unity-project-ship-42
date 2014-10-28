using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private Transform skipButton = null, okButton = null, playButton = null;
    [SerializeField]
    private Transform tipsTitle = null;

    [SerializeField]
    private GameObject[] tips;

    [SerializeField]
    private ShipMovement shipMovement;

    private Camera mainCamera;
    private int tipsIndex;

    void Start()
    {
        mainCamera = Camera.main;

        tipsTitle.position = new Vector2(0, mainCamera.orthographicSize / 2 + tipsTitle.renderer.bounds.size.y * 2f);
        skipButton.position = new Vector2(mainCamera.aspect * mainCamera.orthographicSize - skipButton.renderer.bounds.size.x / 2 - 0.6f,
                                          mainCamera.orthographicSize - skipButton.renderer.bounds.size.y / 2 - 0.75f);

        playButton.gameObject.SetActive(false);
        playButton.position = new Vector2(0, -mainCamera.orthographicSize / 2 + playButton.renderer.bounds.size.y);
        okButton.position = new Vector2(0, -mainCamera.orthographicSize / 2 + okButton.renderer.bounds.size.y);

        tips[tipsIndex].SetActive(true);

        ResizeGuiText();
    }

    private void ResizeGuiText()
    {
        GUIText[] guiTextTutorials = GetComponentsInChildren<GUIText>();
        foreach (GUIText guiText in guiTextTutorials)
        {
            guiText.fontSize = Screen.width / 60;
        }
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

    private void CheckAction(Vector2 position)
    {
        if (okButton.gameObject.activeInHierarchy && position.HasActivated(okButton.position, okButton.renderer.bounds.size, true, true))
        {
            SetNextTip();
        }
        else if (playButton.gameObject.activeInHierarchy && position.HasActivated(playButton.position, playButton.renderer.bounds.size, true, true) ||
                 position.HasActivated(skipButton.position, skipButton.renderer.bounds.size, true, true))
        {
            Application.LoadLevel(SceneName.Level.ToString());
        }
    }

    private void SetNextTip()
    {
        if (tipsIndex < tips.Length - 1)
        {
            tips[tipsIndex].SetActive(false);

            tipsIndex++;

            tips[tipsIndex].SetActive(true);

            ResizeGuiText();

            if (tipsIndex == tips.Length - 1)
            {
                okButton.gameObject.SetActive(false);
                playButton.gameObject.SetActive(true);

                shipMovement.enabled = true;
            }
        }
    }
}
