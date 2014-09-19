using UnityEngine;
using System.Collections;

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
            Destroy(gameObject);
        }

        if (Application.loadedLevelName == "Score")
        {
            int sailedTime = (int)GameSettings.Instance.sailedTime;

            GUIText guiTextScore = GameObject.Find("GUI Text Score").guiText;
            guiTextScore.text = string.Format(guiTextScore.text, sailedTime);

            GUIText guiTextScoreShadow = GameObject.Find("GUI Text Score Shadow").guiText;
            guiTextScoreShadow.text = string.Format(guiTextScoreShadow.text, sailedTime);
        }
    }
}
