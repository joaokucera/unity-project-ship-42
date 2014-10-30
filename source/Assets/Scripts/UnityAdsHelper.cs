// UnityAdsHelper.cs - Written for Unity Ads Asset Store v1.0.2 (SDK 1.3.8)
//  by Nikkolai Davenport <nikkolai@unity3d.com>
//
// Setup Instructions:
// 1. Attach this script to a new game object.
// 2. Enter your game ID into the field provided.
// 
// Usage Guide:
//  Write a script and call UnityAdsHelper.ShowAd() to show an ad. 
//  Customize the HandleShowResults method to perform actions based 
//  on whether an ad was succesfully shown or not.
//
// Notes:
//  - The various debug levels and test mode are only used when
//     Development Build is enabled in Build Settings.
//  - Test mode can be disabled while Development Build is set
//     by checking the option to disable it in the inspector.

using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsHelper : MonoBehaviour
{
    public string gameID;
    public bool disableTestMode;
    public bool showInfoLogs;
    public bool showDebugLogs;
    public bool showWarningLogs = true;
    public bool showErrorLogs = true;

    void Awake()
    {
        if (!Advertisement.isSupported)
        {
            Debug.Log("Current platform is not supported with Unity Ads.");
        }
        else if (string.IsNullOrEmpty(gameID))
        {
            Debug.LogError("A valid game ID is required to initialize Unity Ads.");
        }
        else
        {
            Advertisement.allowPrecache = true;

            Advertisement.debugLevel = Advertisement.DebugLevel.NONE;
            if (showInfoLogs) Advertisement.debugLevel |= Advertisement.DebugLevel.INFO;
            if (showDebugLogs) Advertisement.debugLevel |= Advertisement.DebugLevel.DEBUG;
            if (showWarningLogs) Advertisement.debugLevel |= Advertisement.DebugLevel.WARNING;
            if (showErrorLogs) Advertisement.debugLevel |= Advertisement.DebugLevel.ERROR;

            bool enableTestMode = Debug.isDebugBuild && !disableTestMode;
            Debug.Log(string.Format("Initializing Unity Ads for game ID {0} with test mode {1}...",
                                    gameID, enableTestMode ? "enabled" : "disabled"));

            Advertisement.Initialize(gameID, enableTestMode);
        }
    }

    public static bool isReady(string zone = null)
    {
        return Advertisement.isReady(zone);
    }

    public static void Show(string zone = null, bool pauseGameDuringAd = true)
    {
        ShowOptions options = new ShowOptions();
        options.pause = pauseGameDuringAd;

        //switch (sceneName)
        //{
        //    case SceneName.Menu:
        //        options.resultCallback = HandleShowResultMenu;
        //        break;
        //    case SceneName.Level:
        //        options.resultCallback = HandleShowResultLevel;
        //        break;
        //}

        options.resultCallback = HandleShowResult;
        Advertisement.Show(zone, options);
    }

    private static void HandleShowResultMenu(ShowResult result)
    {
        HandleShowResult(result);

        //Application.LoadLevel(SceneName.Menu.ToString());
    }

    private static void HandleShowResultLevel(ShowResult result)
    {
        HandleShowResult(result);

        //Application.LoadLevel(SceneName.Level.ToString());
    }

    private static void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
}