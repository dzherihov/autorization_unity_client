using Data;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using SimpleJSON;
using UnityEngine;
using RequestBuilderAPI;
using TMPro;

public class UpdateProfile : MonoBehaviour, ISavedProgress
{
    [SerializeField] private TMP_InputField phoneField;
    [SerializeField] private TMP_InputField nameField;
    [SerializeField] private TMP_InputField emailField;
    
    [SerializeField] private bool requestIsStart = false;
    
    private ISaveLoadService _saveLoadService;
    
    [SerializeField] private OpenNotify notifyScr;

    private void Start()
    {
       
        _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        
        if (requestIsStart)
        {
            StartCoroutine(routine: RequestBuilder.SendRequest("GET", apiPath: APIPath.GetProfilePath, isAuth: true, returnedAction: SetProgressProfile, null, true));
        }
    }
    
    private void SetProgressProfile(JSONNode data, string errorStr)
    {
        _saveLoadService.SaveProgress(data, Constants.UpdateProfileKey);
    }

    public void SendBtn()
    {
        if (phoneField.text != "" || nameField.text != "" || emailField.text != "")
        {
            var form = new WWWForm();
            form.AddField("phone", phoneField.text);
            form.AddField("name", nameField.text);
            form.AddField("email", emailField.text);
            StartCoroutine(routine: RequestBuilder.SendRequest("POST", apiPath: APIPath.UpdateProfilePath, isAuth: true, returnedAction: UpdateData, formData: form, true));
        }
        else
        {
            notifyScr.Notify(Constants.FieldsEmpty, SetNotifyMessage.TypesNotify.Error);
        }
    }

    public void LoadData(PlayerProgress progress)
    {
        phoneField.text = progress.ProfileData.phone;
        emailField.text = progress.ProfileData.email;
        nameField.text = progress.ProfileData.name;
    }

    private void UpdateData(JSONNode itemsData, string errorStr)
    {
        if (errorStr == null)
        {
            _saveLoadService.SaveProgress(itemsData, Constants.UpdateProfileKey);
            notifyScr.Notify(Constants.AccessUpdate,SetNotifyMessage.TypesNotify.Access);
        }
        else
        {
            notifyScr.Notify(errorStr, SetNotifyMessage.TypesNotify.Error);
        }
        
    }


    public void LoadProgress(PlayerProgress progress)
    {
        if(progress != null)
            LoadData(progress);
    }

    public void UpdateProgress(PlayerProgress progress, JSONNode itemsData, string key)
    {
        if (key == Constants.UpdateProfileKey)
        {
            progress.ProfileData = new ProfileData(itemsData);
            LoadData(progress);
        }
    }
}
