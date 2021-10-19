using SimpleJSON;
using UnityEngine;
using RequestBuilderAPI;
using TMPro;

public class Register : MonoBehaviour
{
    [SerializeField] private TMP_InputField phoneField;
    [SerializeField] private TMP_InputField passwordField;
    [SerializeField] private TMP_InputField nameField;
    [SerializeField] private TMP_InputField emailField;

    [SerializeField] private OpenNotify NotifyScr;
    
    [SerializeField] private TMP_Text errorText;

    public void SendBtn()
    {
        if (phoneField.text != "" || passwordField.text != "" || nameField.text != "" || emailField.text != "")
        {
            var form = new WWWForm();
            form.AddField("phone", phoneField.text);
            form.AddField("password", passwordField.text);
            form.AddField("name", nameField.text);
            form.AddField("email", emailField.text);
            StartCoroutine(routine: RequestBuilder.SendRequest("POST", apiPath: APIPath.RegisterPath, isAuth: false, returnedAction: SaveToken, formData: form));
        }
        else
        {
            errorText.text = Constants.FieldsEmpty;
            //NotifyScr.Notify(Constants.FieldsEmpty,SetNotifyMessage.TypesNotify.Error);
        }
    }

    private void ResetFields()
    {
        phoneField.text = null;
        passwordField.text = null;
        nameField.text = null;
        emailField.text = null;
    }

    private void SaveToken(JSONNode itemsData, string errorStr)
    {
        errorText.text = errorStr;
        string authToken = itemsData["token_type"] + " " + itemsData["token"];
        PlayerPrefs.SetString("authToken", authToken);
        Debug.Log(authToken);
        Debug.Log(itemsData);
        PlayerPrefs.SetString(Constants.LevelNameKey, ScenesConst.Profile);
        if (errorStr == null)
        {
            NotifyScr.Notify(itemsData["message"], SetNotifyMessage.TypesNotify.Access);
            ResetFields();
            GetComponent<SwitchPanels>().Switch();
        }

        // SceneManager.LoadScene(ScenesConst.Profile);
    }
}
