using System;
using SimpleJSON;

namespace Data
{
  [Serializable]
  public class PlayerProgress
  {
    public ProfileData ProfileData;
    public GameData GameData;

    public PlayerProgress(string initialLevel, JSONNode profileData)
    {
      GameData = new GameData(initialLevel);
      ProfileData = new ProfileData(profileData);
    }
  }
}