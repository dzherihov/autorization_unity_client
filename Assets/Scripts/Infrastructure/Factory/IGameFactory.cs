using System.Collections.Generic;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.Factory
{
  public interface IGameFactory : IService
  {
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }
    void CleanUp();
    void FindWithTagRegistered(string tag);
    void FindWithNameRegistered(string name);
    void GameObjectRegistered(GameObject obj);
  }
}