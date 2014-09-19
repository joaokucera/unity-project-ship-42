using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    private const float maxAudioVolume = 0.5f;

    private bool lastStatus;

    void Start()
    {
        AudioClip sceneClip = Resources.Load<AudioClip>(string.Format("Musics/{0}", Application.loadedLevelName));

        audio.clip = sceneClip;
        audio.volume = 0;
        if (sceneClip.name != "Score")
        {
            audio.loop = true;
        }
        audio.Play();

        StartCoroutine(IncreaseVolume());
    }

    void Update()
    {
        if (GameSettings.Instance.musicEnabled != lastStatus)
        {
            if (GameSettings.Instance.musicEnabled)
            {
                audio.Play();
            }
            else
            {
                audio.Pause();
            }
        }

        lastStatus = GameSettings.Instance.musicEnabled;
    }

    IEnumerator IncreaseVolume()
    {
        while (audio.volume < maxAudioVolume)
        {
            audio.volume += Time.deltaTime;

            yield return 0;
        }

        audio.volume = Mathf.Clamp(audio.volume, 0, maxAudioVolume);
    }
}
