using UnityEngine;
using System.Collections;

public class Tips : MonoBehaviour
{
    [SerializeField]
    private Transform lightBox = null;

    [SerializeField]
    private GameObject pauseObject = null, playerShotSpawnerObject = null;

    [SerializeField]
    private SpriteRenderer closeButton = null;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        pauseObject.SetActive(false);
        playerShotSpawnerObject.SetActive(false);

        Vector2 modalScale = lightBox.localScale;
        modalScale.x = mainCamera.aspect * mainCamera.orthographicSize / 2;
        modalScale.y = mainCamera.aspect * mainCamera.orthographicSize / 2;
        lightBox.localScale = modalScale;

        string aspect = mainCamera.aspect.ToString("0.00");
        int fontSize = 1;

        if (aspect == (3f / 2f).ToString("0.00"))
        {
            fontSize = (int)(mainCamera.aspect * 11.5f);
        }
        else if (aspect == (16f / 10f).ToString("0.00"))
        {
            fontSize = (int)(mainCamera.aspect * 11f);
        }
        else if (aspect == (16f / 9f).ToString("0.00"))
        {
            fontSize = (int)(mainCamera.aspect * 10f);
        }
        else if (aspect == (4f / 3f).ToString("0.00"))
        {
            fontSize = (int)(mainCamera.aspect * 13f);
        }
        else if (aspect == (17f / 10f).ToString("0.00"))
        {
            fontSize = (int)(mainCamera.aspect * 10.5f);
        }

        GUIText[] childrenGuiText = GetComponentsInChildren<GUIText>();
        for (int i = 0; i < childrenGuiText.Length; i++)
        {
            childrenGuiText[i].fontSize = fontSize;
        }

        Time.timeScale = 0f;
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
        if (position.HasActivated(closeButton.transform.position, closeButton.bounds.size, false, true))
        {
            OnInvisible();
        }
    }

    public void OnInvisible()
    {
        Time.timeScale = 1f;

        pauseObject.SetActive(true);
        playerShotSpawnerObject.SetActive(true);

        gameObject.SetActive(false);
    }
}
