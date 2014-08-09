using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SceneName
{
    Enabled,
    Disabled,
    Menu,
    About,
    Level,
    Exit,
    Modal
}

public abstract class GenericScreen : MonoBehaviour
{
    [SerializeField]
    private Transform title;
    [SerializeField]
    private SpriteRenderer giftBoxRenderer;
    [SerializeField]
    private List<Sprite> giftBoxSprites;
    [SerializeField]
    private Modal modalScript;

    protected Camera mainCamera;
    protected SceneName sceneName = SceneName.Enabled;

    /// <summary>
    /// Checks the action.
    /// </summary>
    /// <param name="position">Position.</param>
    protected abstract void CheckAction(Vector2 position);

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        if (sceneName == SceneName.Disabled) return;

        if (sceneName == SceneName.Enabled)
        {
#if UNITY_EDITOR
            CheckAction(Controls.MouseAction());
#else
            CheckAction(Controls.TouchAction());
#endif
        }
        else if (sceneName == SceneName.Modal)
        {
            sceneName = SceneName.Disabled;
            
            modalScript.OnVisible();
        }
        else
        {
            Invoke("LoadLevel", 0f);
        }
    }

    protected void Initialize()
    {
        mainCamera = Camera.main;

        title.position = new Vector2(mainCamera.aspect * mainCamera.orthographicSize / 2,
                                      (mainCamera.orthographicSize + title.renderer.bounds.size.y) / 2);

        if (giftBoxSprites == null || giftBoxSprites.Count == 0)
        {
            Debug.LogError("There are no gift box sprites available!");
        }

        int index = Random.Range(0, giftBoxSprites.Count);
        giftBoxRenderer.sprite = giftBoxSprites[index];
    }

    protected void LoadLevel()
    {
        if (sceneName == SceneName.Exit)
        {
            Application.Quit();
        }
        else
        {
            Application.LoadLevel(sceneName.ToString());
        }

        CancelInvoke("LoadLevel");
    }

    protected bool HasActivated(Vector2 positionA, Vector2 positionB, Vector2 size)
    {
        return Mathf.Abs(positionA.x - positionB.x) <= size.x / 2 &&
               Mathf.Abs(positionA.y - positionB.y) <= size.y / 2;
    }

    protected void ActivateButton(Renderer buttonRenderer, Sprite buttonSpriteActivated, SceneName sceneToLoad)
    {
        ((SpriteRenderer)buttonRenderer).sprite = buttonSpriteActivated;
        sceneName = sceneToLoad;
    }
}