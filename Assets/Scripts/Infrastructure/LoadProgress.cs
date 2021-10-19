using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;

public class LoadProgress : MonoBehaviour
{
    private IPersistentProgressService _progressService;
    private IGameFactory _gameFactory;
    
    private void Awake()
    {
        _progressService =  AllServices.Container.Single<IPersistentProgressService>();
        _gameFactory =  AllServices.Container.Single<IGameFactory>();
        _gameFactory.GameObjectRegistered(this.gameObject);
        _gameFactory.FindWithTagRegistered(Constants.RegisteredSaveObjectsTag);
        foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            progressReader.LoadProgress(_progressService.Progress);
    }
}
