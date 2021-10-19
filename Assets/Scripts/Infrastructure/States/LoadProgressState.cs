using Data;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using RequestBuilderAPI;
using SimpleJSON;
using UI.Logic;
using UnityEngine;

namespace Infrastructure.States
{
  public class LoadProgressState : IState
  {
    private readonly GameStateMachine _gameStateMachine;
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _saveLoadService;
    private readonly LoadingCurtain _curtain;

    public LoadProgressState(GameStateMachine gameStateMachine, LoadingCurtain curtain, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
    {
      _gameStateMachine = gameStateMachine;
      _progressService = progressService;
      _saveLoadService = saveLoadService;
      _curtain = curtain;
    }

    public void Enter()
    {
      _curtain.Show();
      LoadProgress();
      if (!PlayerPrefs.HasKey(Constants.LevelNameKey))
        PlayerPrefs.SetString(Constants.LevelNameKey, ScenesConst.Profile);
    }

    public void Exit()
    {
      _curtain.Hide();
    }

    private JSONNode profileData;
    private void LoadProgress()
    {
      _curtain.StartCoroutine(routine: RequestBuilder.SendRequest("GET", apiPath: APIPath.GetProfilePath, isAuth: true, returnedAction: SetProgressProfile, null, true));
    }

    private void SetProgressProfile(JSONNode data, string errorStr)
    {
      profileData = data;
     InitProgress();
    }

    private void InitProgress()
    {
      _progressService.Progress = NewProgress(profileData);
      _gameStateMachine.Enter<LoadLevelState, string>(PlayerPrefs.GetString(key: Constants.LevelNameKey));
    }
    private static PlayerProgress NewProgress(JSONNode profileData)
    {
      return new PlayerProgress(initialLevel: PlayerPrefs.GetString(key: Constants.LevelNameKey), profileData);
    }
    

  }
}