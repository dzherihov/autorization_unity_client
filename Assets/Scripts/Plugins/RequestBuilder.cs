using System;
using System.Collections;
using Infrastructure;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace RequestBuilderAPI
{
  public static class RequestBuilder
  {
    private static string m_apiToken;

    public static string MApiToken
    {
      get
      {
          if (!PlayerPrefs.HasKey("authToken"))
          {
            Debug.LogError("Unauth");
            m_apiToken = "";
            SceneManager.LoadScene(ScenesConst.Login);
          }
          else 
            m_apiToken = PlayerPrefs.GetString("authToken");

          return m_apiToken;
      }
    }

    public static IEnumerator SendRequest(
      string type, 
      string apiPath, 
      bool isAuth, 
      Action<JSONNode, string> returnedAction,
      WWWForm formData = null,
      bool unauthExit = false
      ) {
      if (returnedAction == null) throw new ArgumentNullException(nameof(returnedAction));
      
      UnityWebRequest www;
      switch (type)
      {
        case "GET":
          www = UnityWebRequest.Get(apiPath);
          break;
        case "POST":
          www = UnityWebRequest.Post(apiPath, formData);
          break;
        default:
          Debug.LogError(type + " type of request does not exist");
          yield break;
          break;
      }

      if (isAuth)
      {
        www.SetRequestHeader("Authorization", MApiToken);
        //Debug.Log(MApiToken);
      }
        

      yield return www.SendWebRequest();

      JSONNode itemsData = JSON.Parse(www.downloadHandler.text);
      
      // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
      string errorField = ErrorHandler(www, itemsData, unauthExit);
      
      // if (error != null)
      // {
      //   errorField = error;
      //   //yield break;
      // }
      
      // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
      Debug.Log( type + ": " + apiPath + ": " + itemsData.ToString());
      //returnedAction(itemsData);
      returnedAction?.Invoke(itemsData, errorField);
      
    }
    
    private static string ErrorHandler(UnityWebRequest www, JSONNode itemsData, bool unauthExit)
    {
      if (www.result != UnityWebRequest.Result.Success) {
        Debug.LogError(www.error);
        if (www.error == "Cannot resolve destination host" || www.error == "Cannot connect to destination host")
        {
          GameBootstrapper.Instance.DestroyObj();
          SceneManager.LoadScene(ScenesConst.DisconnectScene);
        }
        Debug.Log(www.downloadHandler.text);
        if (itemsData["code"] == "auth/unauthorised")
        {
          Debug.LogError(itemsData["code"]);
          if (unauthExit)
          {
            PlayerPrefs.SetString(Constants.LevelNameKey, ScenesConst.Login);
            SceneManager.LoadScene(ScenesConst.Login);
          }

          return itemsData["message"];
        }
        
        if (itemsData["message"] != null)
        {
          return itemsData["message"];
        }
        else
        {
          return www.error;
        }
      }
      
      return null;
    }
    
  }
}
