using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Menu : GenericScreen
{
    [SerializeField]
    private Transform playButton = null, aboutButton = null, exitButton = null, settingsButton = null;
    [SerializeField]
    private Sprite playButtonActivated = null, aboutButtonActivated = null, exitButtonActivated = null, settingsButtonActivated = null;

    void Start()
    {
        if (playButton == null || aboutButton == null || exitButton == null)
        {
            Debug.LogError("There are no transform buttons available!");
        }

        if (playButtonActivated == null || aboutButtonActivated == null || exitButtonActivated == null || settingsButtonActivated == null)
        {
            Debug.LogError("There are no sprite buttons available!");
        }

        base.Initialize();

        playButton.position = new Vector2(mainCamera.aspect * mainCamera.orthographicSize / 2, playButton.position.y);
        aboutButton.position = new Vector2(mainCamera.aspect * mainCamera.orthographicSize / 2, aboutButton.position.y);
        exitButton.position = new Vector2(mainCamera.aspect * mainCamera.orthographicSize / 2, exitButton.position.y);
        settingsButton.position = new Vector2(mainCamera.aspect * -mainCamera.orthographicSize / 2, settingsButton.position.y);
    }

    protected override void CheckAction(Vector2 position)
    {
        if (position.HasActivated(playButton.position, playButton.renderer.bounds.size, true, true))
        {
            ActivateButton(playButton.renderer, playButtonActivated, SceneName.Level);
        }
        else if (position.HasActivated(aboutButton.position, aboutButton.renderer.bounds.size, true, true))
        {
            ActivateButton(aboutButton.renderer, aboutButtonActivated, SceneName.About);
        }
        else if (position.HasActivated(exitButton.position, exitButton.renderer.bounds.size, true, true))
        {
            ActivateButton(exitButton.renderer, exitButtonActivated, SceneName.Exit);
        }
        else if (position.HasActivated(settingsButton.position, settingsButton.renderer.bounds.size, true, true))
        {
            ActivateButton(settingsButton.renderer, settingsButtonActivated, SceneName.Modal);
        }
    }

    public void ReactiveButtons()
    {
        sceneName = SceneName.Enabled;
    }
}