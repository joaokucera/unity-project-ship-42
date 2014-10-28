using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SceneName
{
    Enabled,
    Disabled,
    Menu,
    About,
    Tutorial,
    Level,
    Exit,
    Modal,
    Score
}

public abstract class GenericScreen : MonoBehaviour
{
    [SerializeField]
    private Transform title = null;
    [SerializeField]
    private SpriteRenderer giftBoxRenderer = null;
    [SerializeField]
    private List<Sprite> giftBoxSprites = null;
    [SerializeField]
    private Modal modalScript = null;

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
        else if (sceneName == SceneName.Modal)
        {
            sceneName = SceneName.Disabled;

            modalScript.OnVisible();
        }
        else
        {   
            if (sceneName == SceneName.Exit)
            {
                Application.Quit();
            }
            else
            {
                Application.LoadLevel(sceneName.ToString());
            }
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

    protected void ActivateButton(Renderer buttonRenderer, Sprite buttonSpriteActivated, SceneName sceneToLoad)
    {
        ((SpriteRenderer)buttonRenderer).sprite = buttonSpriteActivated;
        sceneName = sceneToLoad;
    }
}
