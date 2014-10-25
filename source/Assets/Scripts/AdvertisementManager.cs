using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class AdvertisementManager : MonoBehaviour
{
    private const string GameIDForAndroid = "18580";
    private const string GameIDForIOs = "";

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

    public bool testModeEnabled;

    private SceneName sceneToLoad;
    private bool showAds;

    void Awake()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.allowPrecache = true;
            Advertisement.debugLevel = Advertisement.DebugLevel.ERROR;

            bool enableTestMode = Debug.isDebugBuild && testModeEnabled;
            Advertisement.Initialize(GameIDForAndroid, enableTestMode);
        }
    }

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
    }

    void Update()
    {
        if (Advertisement.isReady() && showAds)
        {
            showAds = false;

            Advertisement.Show(null, new ShowOptions
            {
                pause = true,
                resultCallback = result => { }
            });
        }
    }

    public void ShowAds(SceneName sceneName)
    {
        showAds = true;
        sceneToLoad = sceneName;

        Time.timeScale = 1f;
        Application.LoadLevel(sceneToLoad.ToString());
    }
}