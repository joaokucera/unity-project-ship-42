using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class About : GenericScreen {

	[SerializeField] private Transform backButton;
	[SerializeField] private Sprite backButtonActivated;

	void Start()
	{
		if (backButtonActivated == null)
		{
			Debug.LogError("There is not sprite button available!");
		}

		base.Initialize ();

		backButton.position = new Vector2 (mainCamera.aspect * mainCamera.orthographicSize / 2, backButton.position.y);
	}

	protected override void CheckAction (Vector2 position)
	{
		if (HasActivated (position, backButton.position, backButton.renderer.bounds.size)) 
		{
			ActivateButton(backButton.renderer, backButtonActivated, SceneName.Menu);
		}
	}
}