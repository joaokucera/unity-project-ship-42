using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SceneName
{
	Menu,
	Credits,
	Level
}

public class Menu : GenericScreen {

	[SerializeField] private Transform playButton;
	[SerializeField] private Sprite playButtonActivated;
	[SerializeField] private Transform creditsButton;
	[SerializeField] private Sprite creditsButtonActivated;

	private bool buttonActivated = false;
	private SceneName sceneName = SceneName.Menu;

	void Start()
	{
		base.Initialize ();

		playButton.position = new Vector2 (mainCamera.aspect * mainCamera.orthographicSize - playButton.renderer.bounds.size.x, 
		                                   -mainCamera.orthographicSize / 2 + playButton.renderer.bounds.size.y * 2);
		
		creditsButton.position = new Vector2 (mainCamera.aspect * mainCamera.orthographicSize - creditsButton.renderer.bounds.size.x + 0.5f, 
		                                      -mainCamera.orthographicSize / 2 - creditsButton.renderer.bounds.size.y);
	}

	void Update()
	{
		if (buttonActivated)
		{
			Invoke("LoadLevel", 0.25f);
		}
		else
		{
#if UNITY_EDITOR
			MouseAction();
#else
			TouchAction ();
#endif
		}
	}

	private void LoadLevel()
	{
		Application.LoadLevel (sceneName.ToString());
	}
	
	private void MouseAction ()
	{
		// Just 1 tap.
		if (Input.GetButtonDown("Fire1"))
		{
			Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

			if (HasActivated(mousePosition, playButton.position, playButton.renderer.bounds.size))
			{
				((SpriteRenderer)playButton.renderer).sprite = playButtonActivated;
				sceneName = SceneName.Level;
				buttonActivated = true;

			}
			else if (HasActivated(mousePosition, creditsButton.position, creditsButton.renderer.bounds.size))
			{
				((SpriteRenderer)creditsButton.renderer).sprite = creditsButtonActivated;
				sceneName = SceneName.Credits;
				buttonActivated = true;
			}
		}
	}
	
	private void TouchAction ()
	{
		// Just 1 tap.
		if (Input.touchCount == 1)
		{
			Touch touch = Input.GetTouch(0);

			if (touch.phase == TouchPhase.Began)
			{
				if (HasActivated(touch.position, playButton.position, playButton.renderer.bounds.size))
				{
					((SpriteRenderer)playButton.renderer).sprite = playButtonActivated;
				}
				else if (HasActivated(touch.position, creditsButton.position, creditsButton.renderer.bounds.size))
				{
					((SpriteRenderer)creditsButton.renderer).sprite = creditsButtonActivated;
				}
			}
		}
	}

	private bool HasActivated(Vector2 positionA, Vector2 positionB, Vector2 size)
	{
		return Mathf.Abs(positionA.x - positionB.x) <= size.x / 2 && 
			   Mathf.Abs(positionA.y - positionB.y) <= size.y / 2;
	}
}