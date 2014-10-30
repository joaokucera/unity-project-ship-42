using UnityEngine;
using System.Collections;

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

    private bool showAds = false;

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
        if (showAds)
        {
            if (UnityAdsHelper.isReady())
            {
                UnityAdsHelper.Show();
            }

            showAds = false;
        }
    }

    public void ShowAds(SceneName sceneName)
    {
        showAds = true;
        Time.timeScale = 1f;

        Application.LoadLevel(sceneName.ToString());
    }
}