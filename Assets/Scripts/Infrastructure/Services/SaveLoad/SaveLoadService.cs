using Data;
using Infrastructure.Factory;
using Infrastructure.Services.PersistentProgress;
using SimpleJSON;
using UnityEngine;

namespace Infrastructure.Services.SaveLoad
{
  public class SaveLoadService : ISaveLoadService
  {
    private readonly IPersistentProgressService _progressService;
    private readonly IGameFactory _gameFactory;

    public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
    {
      _progressService = progressService;
      _gameFactory = gameFactory;
    }
    
    public void SaveProgress(JSONNode itemsData, string key)
    {
      foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
        progressWriter.UpdateProgress(_progressService.Progress, itemsData, key);
      
      PlayerPrefs.SetString(Constants.ProgressKey, _progressService.Progress.ToJson());
    }

    public PlayerProgress LoadProgress() =>
      PlayerPrefs.GetString(Constants.ProgressKey)?
        .ToDeserialized<PlayerProgress>();
  }
}