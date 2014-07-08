using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Menu : GenericScreen {
	
	[SerializeField] private Transform playButton = null, aboutButton = null, exitButton = null;
	[SerializeField] private Sprite playButtonActivated = null, aboutButtonActivated = null, exitButtonActivated = null;

	void Start()
	{
		if (playButton == null || aboutButton == null || exitButton == null)
		{
			Debug.LogError("There are no transform buttons available!");
		}

		if (playButtonActivated == null || aboutButtonActivated == null || exitButtonActivated == null)
		{
			Debug.LogError("There are no sprite buttons available!");
		}

		base.Initialize ();

		playButton.position = new Vector2 (mainCamera.aspect * mainCamera.orthographicSize / 2, playButton.position.y);
		aboutButton.position = new Vector2 (mainCamera.aspect * mainCamera.orthographicSize / 2, aboutButton.position.y);
		exitButton.position = new Vector2 (mainCamera.aspect * mainCamera.orthographicSize / 2, exitButton.position.y);
	}

	protected override void CheckAction (Vector2 position)
	{
		if (HasActivated (position, playButton.position, playButton.renderer.bounds.size)) 
		{
			ActivateButton(playButton.renderer, playButtonActivated, SceneName.Level);
		}
		else if (HasActivated (position, aboutButton.position, aboutButton.renderer.bounds.size)) 
		{
			ActivateButton(aboutButton.renderer, aboutButtonActivated, SceneName.About);
		}
		else if (HasActivated (position, exitButton.position, exitButton.renderer.bounds.size)) 
		{
			ActivateButton(exitButton.renderer, exitButtonActivated, SceneName.Exit);
		}
	}
}