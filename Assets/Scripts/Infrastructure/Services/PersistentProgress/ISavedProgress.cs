using Data;
using SimpleJSON;

namespace Infrastructure.Services.PersistentProgress
{
  public interface ISavedProgressReader
  {
    void LoadProgress(PlayerProgress progress);
  }

  public interface ISavedProgress : ISavedProgressReader
  {
    void UpdateProgress(PlayerProgress progress, JSONNode itemsData, string key);
  }
}