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

    void Start()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this);
        }
        else
        {
            //Destroy(gameObject);
        }

        //if (Application.loadedLevelName == "Score")
        //{
        //    int sailedTime = (int)GameSettings.Instance.sailedTime;
        //    TimeSpan time = new TimeSpan(0, 0, sailedTime);

        //    GUIText guiTextScore = GameObject.Find("GUI Text Score").guiText;
        //    guiTextScore.text = string.Format(guiTextScore.text, time.Minutes.ToString("00"), time.Seconds.ToString("00"));

        //    GUIText guiTextScoreShadow = GameObject.Find("GUI Text Score Shadow").guiText;
        //    guiTextScoreShadow.text = string.Format(guiTextScoreShadow.text, time.Minutes.ToString("00"), time.Seconds.ToString("00"));
        //}
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
            else if (Application.loadedLevelName == SceneName.Score.ToString())
            {
                Application.LoadLevel(SceneName.Menu.ToString());
            }
            if (Application.loadedLevelName == SceneName.Level.ToString())
            {
                Application.LoadLevel(SceneName.Menu.ToString());
            }
        }
    }
}
