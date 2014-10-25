using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.Advertisements {

  using DebugLevel = Advertisement.DebugLevel;

  internal static class Utils {

    public static string addUrlParameters(string url, Dictionary<string, object> parameters) {
      if(url.IndexOf('?') != -1) {
        url += "&";
      } else {
        url += "?";
      }

      List<string> pairs = new List<string>();
      foreach(KeyValuePair<string, object> entry in parameters) {
        if(entry.Value != null) {
          pairs.Add(entry.Key + "=" + entry.Value.ToString());
        }
      }

      return url + String.Join("&", pairs.ToArray());
    }

    public static string Join(IEnumerable enumerable, string separator) {
      string result = "";
      foreach(object entry in enumerable) {
        result += entry.ToString() + separator;
      }
      return result.Length > 0 ? result.Substring(0, result.Length - separator.Length) : result;
    }

    public static T Optional<T>(Dictionary<string, object> jsonObject, string key, object defaultValue = null) {
      try {
        return (T)jsonObject[key];
      } catch {
        return (T)defaultValue;
      }
    }

    private static void Log(DebugLevel debugLevel, string message) {
      if((Advertisement.debugLevel & debugLevel) != DebugLevel.NONE) {
        Debug.Log(message);
      }
    }

    public static void LogDebug(string message) {
      Log (DebugLevel.DEBUG, message);
    }

    public static void LogInfo(string message) {
      Log (DebugLevel.INFO, message);
    }

    public static void LogWarning(string message) {
      Log (DebugLevel.WARNING, message);
    }

    public static void LogError(string message) {
      Log (DebugLevel.ERROR, message);
    }

  }

}
