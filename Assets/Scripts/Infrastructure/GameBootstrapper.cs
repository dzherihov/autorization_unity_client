using Infrastructure.States;
using UI.Logic;
using UnityEngine;

namespace Infrastructure
{
  public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
  {
    public static GameBootstrapper Instance;
    
    public LoadingCurtain CurtainPrefab;
    
    private Game _game;

    private void Awake()
    {
      _game = new Game(this, Instantiate(CurtainPrefab));
      _game.StateMachine.Enter<BootstrapState>();
      
      Singleton();
    }
    
    public void DestroyObj()
    {
      Destroy(gameObject);
    }

    private void Singleton()
    {
      if (Instance == null) Instance = this; 
      else
      {
        Destroy(Instance.gameObject);
        Instance = this;
      }
      DontDestroyOnLoad(this);
    }
  }
}