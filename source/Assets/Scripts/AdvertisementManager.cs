using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class AdvertisementManager : MonoBehaviour
{
    private static AdvertisementManager instance;
    public static AdvertisementManager Instance
    {
        get
        {
            if (AdvertisementManager.instance == null)
            {
                AdvertisementManager.instance = GameObject.Find("Advertisement Manager").GetComponent<AdvertisementManager>();
            }

            return AdvertisementManager.instance;
        }
    }

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

    public void ShowAds(SceneName sceneName)
    {
        Time.timeScale = 1f;

        UnityAdsHelper.Show(sceneName);
    }
}