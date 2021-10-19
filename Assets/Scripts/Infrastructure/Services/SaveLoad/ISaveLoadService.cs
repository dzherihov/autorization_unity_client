using Data;
using SimpleJSON;

namespace Infrastructure.Services.SaveLoad
{
  public interface ISaveLoadService : IService
  {
    void SaveProgress(JSONNode itemsData, string key);
    PlayerProgress LoadProgress();
  }
}