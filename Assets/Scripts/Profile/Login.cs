using Infrastructure;
using SimpleJSON;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using RequestBuilderAPI;
using TMPro;

public class Login : MonoBehaviour
{
    [SerializeField] private TMP_Text errorText;
    
    [SerializeField] private TMP_InputField emailField;
    [SerializeField] private TMP_InputField passwordField;

    public void SendBtn()
    {
        if(emailField.text != "" || passwordField.text != "")
        {
            var bootstrapper = FindObjectOfType<GameBootstrapper>();
            if(bootstrapper)
                Destroy(bootstrapper.gameObject);
            var form = new WWWForm();
            form.AddField("email", emailField.text);
            form.AddField("password", passwordField.text);
            form.AddField("remember_me", "true");
            StartCoroutine(routine: RequestBuilder.SendRequest("POST", apiPath: APIPath.LoginPath, isAuth: false, returnedAction: SaveToken, formData: form));
        }
        else
        {
            errorText.text = Constants.FieldsEmpty;
        }
            
    }

    private void SaveToken(JSONNode itemsData, string errorStr)
    {
        errorText.text = errorStr;
        if(errorStr != null) return;
        string authToken = itemsData["token_type"] + " " + itemsData["token"];
        PlayerPrefs.SetString("authToken", authToken);
        Debug.Log(authToken);
        PlayerPrefs.SetString(Constants.LevelNameKey, ScenesConst.Profile);
        SceneManager.LoadScene(ScenesConst.Profile);
    }
    
}