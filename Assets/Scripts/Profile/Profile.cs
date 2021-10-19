using Data;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using RequestBuilderAPI;
using SimpleJSON;
using TMPro;
using UnityEngine;

public class Profile : MonoBehaviour, ISavedProgress
{
  [SerializeField] private TMP_Text phoneText;
  [SerializeField] private TMP_Text emailText;
  [SerializeField] private TMP_Text nameText;
  [SerializeField] private TMP_Text nameAvatarText;
  [SerializeField] private TMP_Text errorText;

  [SerializeField] private bool requestIsStart = false;
  
  public ProfileData Data;
  
  private ISaveLoadService _saveLoadService;

  private void Start()
  {
    _saveLoadService = AllServices.Container.Single<ISaveLoadService>();

    if (requestIsStart)
    {
      StartCoroutine(routine: RequestBuilder.SendRequest("GET", apiPath: APIPath.GetProfilePath, isAuth: true, returnedAction: SetProgressProfile, null, true));
    }
  }

  public void SendRequest()
  {
    StartCoroutine(routine: RequestBuilder.SendRequest("GET", apiPath: APIPath.GetProfilePath, isAuth: true, returnedAction: SetProgressProfile, null, true));

  }
  private void SetProgressProfile(JSONNode data, string errorStr)
  {
    _saveLoadService.SaveProgress(data, Constants.UpdateProfileKey);
  }

  public void LoadData(ProfileData progress)
  {
    Data = progress;
    if(phoneText) phoneText.text = progress.phone;
    if(emailText) emailText.text = progress.email;
    if(nameText) nameText.text = progress.name;
    if(nameAvatarText) nameAvatarText.text = progress.name_avatar;
  }

  public void LoadProgress(PlayerProgress progress)
  {
    if(progress != null)
      LoadData(progress.ProfileData);
  }

  public void UpdateProgress(PlayerProgress progress, JSONNode itemsData, string key)
  {
    if (key == Constants.UpdateProfileKey)
    {
      progress.ProfileData = new ProfileData(itemsData);
      LoadData(progress.ProfileData);
    }
      
  }
}


