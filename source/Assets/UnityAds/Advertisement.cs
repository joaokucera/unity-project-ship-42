using System;

namespace UnityEngine.Advertisements {

  public static class Advertisement {

    public enum DebugLevel {
      NONE = 0,
      ERROR = 1,
      WARNING = 2,
      INFO = 4,
      DEBUG = 8
    }

    static public DebugLevel debugLevel = DebugLevel.ERROR | DebugLevel.WARNING;

    static public bool isSupported {
      get {
        return 
          Application.isEditor ||
          Application.platform == RuntimePlatform.IPhonePlayer || 
          Application.platform == RuntimePlatform.Android;
      }
    }

    static public bool isInitialized {
      get {
        return Engine.Instance.isInitialized;
      }
    }

    static public void Initialize(string appId, bool testMode = false) {
      Engine.Instance.Initialize(appId, testMode);
    }

    static public void Show(string zoneId = null, ShowOptions options = null) {
      Engine.Instance.Show(zoneId, options);
    }

    static public bool allowPrecache { 
      get {
        return Engine.Instance.allowPrecache;
      }
      set {
        Engine.Instance.allowPrecache = value;
      }
    }

    static public bool isReady(string zoneId = null) {
      return Engine.Instance.isReady(zoneId);
    }

    static public bool isShowing { 
      get {
        return Engine.Instance.isShowing();
      }
    }

  }

}