using UnityEngine;
using System.Collections;
using System;

public class GameSettings : MonoBehaviour
{
    private static GameSettings instance;
    public static GameSettings Instance
    {
        get
        {
            if (GameSettings.instance == null)
            {
                GameSettings.instance = GameObject.Find("Game Settings").GetComponent<GameSettings>();
            }

            return GameSettings.instance;
        }
    }

    [HideInInspector]
    public bool musicEnabled = true;
    [HideInInspector]
    public bool specialEffectsEnabled = true;
    [HideInInspector]
    public bool acceleratorEnabled = false;
    [HideInInspector]
    public bool hapticsEnabled = false;

    /// <summary>
    /// TEMPO DE JOGO.
    /// </summary>
    [HideInInspector]
    public float sailedTime = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Application.loadedLevelName == SceneName.Menu.ToString())
            {
                Application.Quit();
            }
            else if (Application.loadedLevelName == SceneName.About.ToString())
            {
                Application.LoadLevel(SceneName.Menu.ToString());
            }
            else if (Application.loadedLevelName == SceneName.Tutorial.ToString())
            {
                Application.LoadLevel(SceneName.Menu.ToString());
            }
            else if (Application.loadedLevelName == SceneName.Level.ToString())
            {
                Application.LoadLevel(SceneName.Menu.ToString());
            }
        }
    }
}
