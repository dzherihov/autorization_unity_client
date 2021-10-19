using Infrastructure;
using SimpleJSON;
using UnityEngine;
using UnityEngine.SceneManagement;
using RequestBuilderAPI;

public class Logout : MonoBehaviour
{
    //[SerializeField] private Text errorText;
    public void SendBtn()
    {
        var form = new WWWForm();
        StartCoroutine(routine: RequestBuilder.SendRequest("POST", apiPath: APIPath.LogoutPath, isAuth: true, returnedAction: LoadLevel, formData: form, true));
    }
    
    private void LoadLevel(JSONNode itemsData, string errorStr)
    {
       // errorText.text = errorStr;
        var bootstrapper = FindObjectOfType<GameBootstrapper>();
        if(bootstrapper)
            Destroy(bootstrapper.gameObject);
        PlayerPrefs.SetString(Constants.LevelNameKey, ScenesConst.Login);
        SceneManager.LoadScene(ScenesConst.Login);
    }
}
