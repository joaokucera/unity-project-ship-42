#if UNITY_ANDROID

using UnityEngine;
using System.Collections;

namespace UnityEngine.Advertisements {

  internal class UnityAdsAndroid : UnityAdsPlatform {
	private static AndroidJavaObject unityAds;
	private static AndroidJavaObject unityAdsUnity;
	private static AndroidJavaObject currentActivity;
		
	public override void init (string gameId, bool testModeEnabled, bool debugModeEnabled, string gameObjectName) {    
		Utils.LogDebug("UnityAndroid: init(), gameId=" + gameId + ", testModeEnabled=" + testModeEnabled + ", gameObjectName=" + gameObjectName + ", debugModeEnabled=" + debugModeEnabled);
		currentActivity = (new AndroidJavaClass("com.unity3d.player.UnityPlayer")).GetStatic<AndroidJavaObject>("currentActivity");
		unityAdsUnity = new AndroidJavaObject("com.unity3d.ads.android.unity3d.UnityAdsUnityWrapper");
		unityAdsUnity.Call("init", gameId, currentActivity, testModeEnabled, debugModeEnabled, gameObjectName);
	}
		
	public override bool show (string zoneId, string rewardItemKey, string options) {
    Utils.LogDebug ("UnityAndroid: show()");
		return unityAdsUnity.Call<bool>("show", zoneId, rewardItemKey, options);
	}
		
	public override void hide () {
      Utils.LogDebug ("UnityAndroid: hide()");
		unityAdsUnity.Call("hide");
	}
		
	public override bool isSupported () {
      Utils.LogDebug ("UnityAndroid: isSupported()");
		return unityAdsUnity.Call<bool>("isSupported");
	}
		
	public override string getSDKVersion () {
      Utils.LogDebug ("UnityAndroid: getSDKVersion()");
		return unityAdsUnity.Call<string>("getSDKVersion");
	}
		
	public override bool canShowAds () {
      Utils.LogDebug ("UnityAndroid: canShowAds()");
		return unityAdsUnity.Call<bool>("canShowAds");
	}
		
	public override bool canShow () {
      Utils.LogDebug ("UnityAndroid: canShow()");
		return unityAdsUnity.Call<bool>("canShow");
	}
		
	public override bool hasMultipleRewardItems () {
      Utils.LogDebug ("UnityAndroid: hasMultipleRewardItems()");
		return unityAdsUnity.Call<bool>("hasMultipleRewardItems");
	}
		
	public override string getRewardItemKeys () {
      Utils.LogDebug ("UnityAndroid: getRewardItemKeys()");
		return unityAdsUnity.Call<string>("getRewardItemKeys");
	}
		
	public override string getDefaultRewardItemKey () {
      Utils.LogDebug ("UnityAndroid: getDefaultRewardItemKey()");
		return unityAdsUnity.Call<string>("getDefaultRewardItemKey");
	}
		
	public override string getCurrentRewardItemKey () {
      Utils.LogDebug ("UnityAndroid: getCurrentRewardItemKey()");
		return unityAdsUnity.Call<string>("getCurrentRewardItemKey");
	}
		
	public override bool setRewardItemKey (string rewardItemKey) {
      Utils.LogDebug("UnityAndroid: setRewardItemKey() rewardItemKey=" + rewardItemKey);
		return unityAdsUnity.Call<bool>("setRewardItemKey", rewardItemKey);
	}
		
	public override void setDefaultRewardItemAsRewardItem () {
      Utils.LogDebug ("UnityAndroid: setDefaultRewardItemAsRewardItem()");
		unityAdsUnity.Call("setDefaultRewardItemAsRewardItem");
	}
		
	public override string getRewardItemDetailsWithKey (string rewardItemKey) {
      Utils.LogDebug ("UnityAndroid: getRewardItemDetailsWithKey() rewardItemKey=" + rewardItemKey);
		return unityAdsUnity.Call<string>("getRewardItemDetailsWithKey", rewardItemKey);
	}
		
	public override string getRewardItemDetailsKeys () {
      Utils.LogDebug ("UnityAndroid: getRewardItemDetailsKeys()");
		return unityAdsUnity.Call<string>("getRewardItemDetailsKeys");
	}

  public override void setNetwork(string network) {
      Utils.LogDebug("UnityAndroid: setNetwork()");
      unityAdsUnity.Call("setNetwork", network);
  }
  }
}

#endif