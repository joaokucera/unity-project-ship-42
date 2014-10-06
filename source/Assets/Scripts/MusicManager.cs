using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    private const float maxAudioVolume = 0.5f;

    void Start()
    {
        AudioClip sceneClip = Resources.Load<AudioClip>(string.Format("Musics/{0}", Application.loadedLevelName));

        audio.clip = sceneClip;
        audio.volume = 0;
        audio.loop = true;

        PlayOrPause();

        StartCoroutine(IncreaseVolume());
    }

    public void PlayOrPause()
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
