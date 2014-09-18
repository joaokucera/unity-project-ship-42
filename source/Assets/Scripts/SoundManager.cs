using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    private const float maxAudioVolume = 0.5f;

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
