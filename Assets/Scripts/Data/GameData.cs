using System;

namespace Data
{
  [Serializable]
  public class GameData
  {
    public string Level;

    public GameData(string initialLevel)
    {
      Level = initialLevel;
    }
  }
}