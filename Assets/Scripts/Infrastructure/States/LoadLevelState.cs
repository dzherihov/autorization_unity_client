using Infrastructure.Factory;
using Infrastructure.Services.PersistentProgress;
using UI.Logic;

namespace Infrastructure.States
{
  public class LoadLevelState : IPayLoadState<string>
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;
    private readonly IGameFactory _gameFactory;
    private readonly IPersistentProgressService _progressService;

    public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory, IPersistentProgressService progressService)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _curtain = curtain;
      _gameFactory = gameFactory;
      _progressService = progressService;
    }

    public void Enter(string sceneName)
    {
      _curtain.Show();
      _gameFactory.CleanUp();
      _sceneLoader.Load(name: sceneName, onLoaded: OnLoaded);
    }

    public void Exit() => _curtain.Hide();

    private void OnLoaded()
    {

      InitRegisteredSaveObjects();
      
      InformProgressReaders();
      
      _stateMachine.Enter<GameLoopState>();
    }

    private void InitRegisteredSaveObjects()
    {
       _gameFactory.FindWithTagRegistered(Constants.RegisteredSaveObjectsTag);
      // _gameFactory.FindWithNameRegistered(Constants.RequestUpdateProfileName);
       // _gameFactory.FindWithNameRegistered(Constants.RequestsAdminCasinosName);
       // _gameFactory.FindWithNameRegistered(Constants.RequestsAdminCasinosName);
       // _gameFactory.FindWithNameRegistered(Constants.ScriptName);

    }

    private void InformProgressReaders()
    {
      foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
        progressReader.LoadProgress(_progressService.Progress);
    }
    
  }
}