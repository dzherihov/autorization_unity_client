using System.Collections.Generic;
using Infrastructure.AssetManagement;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    private readonly IAssetProvider _assets;

    public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

    public GameFactory(IAssetProvider assets)
    {
      _assets = assets;
    }
    
    // public GameObject CreateHero(GameObject at) =>
    //   InstantiateRegistered(Constants.PlayerPath, at.transform.position);

    // public void InitHero(GameObject hero, int id)
    // {
    //   PlayerScriptableObject playersConfig = Resources.Load<PlayerScriptableObject>(Constants.ConfigPlayers);
    //   PlayerStructure playerItem = playersConfig.players.Find((player) => player.idPlayer == id);
    //   
    //   hero.GetComponent<ShootTeleport>().Bullet = playerItem.BulletPrephabe;
    //   hero.GetComponent<SpriteRenderer>().sprite = playerItem.SpritePlayer;
    //   hero.GetComponent<SpriteGlowEffect>().GlowColor = playerItem.GlowColor;
    // }

    public void FindWithTagRegistered(string tag)
    {
      GameObject[] progressReaders = GameObject.FindGameObjectsWithTag(tag);
      if(progressReaders.Length>0)
        foreach (GameObject gameObject in progressReaders)
          RegisterProgressWatchers(gameObject);
    }    
    
    public void FindWithNameRegistered(string name)
    {
      GameObject gameObject = GameObject.Find(name);
      if(gameObject != null)
        RegisterProgressWatchers(gameObject);
    }
    
    public void GameObjectRegistered(GameObject obj)
    {
      if (obj != null)
      {
        CleanUp();
        foreach (ISavedProgressReader progressReader in obj.GetComponentsInChildren<ISavedProgressReader>())
          ReRegister(progressReader);
      }
    }

    public void CleanUp()
    {
      ProgressReaders.Clear();
      ProgressWriters.Clear();
    }

    private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
    {
      GameObject gameObject = _assets.Instantiate(prefabPath, at: at);
      RegisterProgressWatchers(gameObject);
      return gameObject;
    }    
    
    private GameObject InstantiateRegistered(string prefabPath)
    {
      GameObject gameObject = _assets.Instantiate(prefabPath);
      RegisterProgressWatchers(gameObject);
      return gameObject;
    }

    private void RegisterProgressWatchers(GameObject gameObject)
    {
      foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
        Register(progressReader);
    }

    private void Register(ISavedProgressReader progressReader)
    {
      if (progressReader is ISavedProgress progressWriter)
        ProgressWriters.Add(progressWriter);
        
      ProgressReaders.Add(progressReader);
    }
    
    private void ReRegister(ISavedProgressReader progressReader)
    {
     
      // foreach (var val in ProgressReaders)
      // {
      //   if (val == progressReader || val == null)
      //   {
      //     ProgressReaders.Remove(val);
      //     ProgressReaders.Add(progressReader);
      //   }
      // }
      //
      // foreach (var val in ProgressWriters)
      // {
      //   if (progressReader is ISavedProgress progressWriter2)
      //   {
      //     if (val == progressWriter2 || val == null)
      //     {
      //       ProgressWriters.Remove(val);
      //       ProgressWriters.Add(progressWriter2);
      //     }
      //   }
      // }

      if (progressReader is ISavedProgress progressWriter)
        ProgressWriters.Add(progressWriter);
        
      ProgressReaders.Add(progressReader);

    }
  }
}