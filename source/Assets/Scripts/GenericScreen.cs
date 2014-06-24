using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SceneName
{
	None,
	Menu,
	About,
	Level,
	Exit
}

public abstract class GenericScreen : MonoBehaviour {

	[SerializeField] private GUISkin screenSkin;
	[SerializeField] private Transform title;
	[SerializeField] private SpriteRenderer giftBoxRenderer;
	[SerializeField] private List<Sprite> giftBoxSprites;

	protected Camera mainCamera;
	protected SceneName sceneName = SceneName.None;

	/// <summary>
	/// Checks the action.
	/// </summary>
	/// <param name="position">Position.</param>
	protected abstract void CheckAction(Vector2 position);

	void Start () 
	{
		Initialize ();
	}
	
	void Update()
	{
		if (sceneName == SceneName.None)
		{
#if UNITY_EDITOR
			MouseAction();
#else
			TouchAction ();
#endif
		}
		else
		{
			Invoke("LoadLevel", 0f);
		}
	}
		
	void OnGUI()
	{
		GUI.skin = screenSkin;
		GUI.Label (new Rect(10, 5, 120, 40), "v.0.0.2");
	}

	protected void Initialize ()
	{
		mainCamera = Camera.main;

		title.position = new Vector2 (mainCamera.aspect * mainCamera.orthographicSize / 2, 
		                              (mainCamera.orthographicSize + title.renderer.bounds.size.y) / 2);

		if (giftBoxSprites == null || giftBoxSprites.Count == 0) 
		{
			Debug.LogError ("There are no gift box sprites available!");
		}

		int index = Random.Range (0, giftBoxSprites.Count);
		giftBoxRenderer.sprite = giftBoxSprites [index];
	}

	protected void MouseAction ()
	{
		// Just 1 tap.
		if (Input.GetButtonDown("Fire1"))
		{
			Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
			
			CheckAction (mousePosition);
		}
	}
	
	protected void TouchAction ()
	{
		// Just 1 tap.
		if (Input.touchCount == 1)
		{
			Touch touch = Input.GetTouch(0);
			Vector2 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);
			
			if (touch.phase == TouchPhase.Began)
			{
				CheckAction (touchPosition);
			}
		}
	}

	protected void LoadLevel()
	{
		if (sceneName == SceneName.Exit)
		{
			Application.Quit();
		}
		else
		{
			Application.LoadLevel (sceneName.ToString());
		}

		CancelInvoke ("LoadLevel");
	}
	
	protected bool HasActivated(Vector2 positionA, Vector2 positionB, Vector2 size)
	{
		return Mathf.Abs(positionA.x - positionB.x) <= size.x / 2 && 
			   Mathf.Abs(positionA.y - positionB.y) <= size.y / 2;
	}

	protected void ActivateButton (Renderer buttonRenderer, Sprite buttonSpriteActivated, SceneName sceneToLoad)
	{
		((SpriteRenderer)buttonRenderer).sprite = buttonSpriteActivated;
		sceneName = sceneToLoad;
	}
}