using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
{
    private Camera mainCamera;
    private float xOffset = 5f;

    [SerializeField]
    private Modal modalScript;
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
#if UNITY_EDITOR
        CheckAction(Controls.MouseAction());
#else
		CheckAction(Controls.TouchAction());
#endif
    }

    private void CheckAction(Vector2 position)
    {
        if (HasActivated(position, transform.position, renderer.bounds.size))
        {
            if (Time.timeScale == 1f)
            {
                renderer.enabled = false;
                playButton.enabled = true;
                reloadButton.enabled = true;

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
        else if (HasActivated(position, playButton.transform.position, playButton.bounds.size))
        {
            renderer.enabled = true;
            playButton.enabled = false;
            reloadButton.enabled = false;

            if (modalScript.OnInvisible())
            {
                Time.timeScale = 1f;
            }
        }
        //else if (HasActivated(position, reloadButton.transform.position, reloadButton.bounds.size))
        //{
        //    print("reload");

        //    Time.timeScale = 1f;
        //    Application.LoadLevel("Level");
        //}
    }

    private bool HasActivated(Vector2 positionA, Vector2 positionB, Vector2 size)
    {
        return Mathf.Abs(positionA.x - positionB.x) <= size.x &&
               Mathf.Abs(positionA.y - positionB.y) <= size.y;
    }
}
