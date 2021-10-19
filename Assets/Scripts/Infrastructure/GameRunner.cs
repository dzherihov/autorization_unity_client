using UnityEngine;

namespace Infrastructure
{
  public class GameRunner : MonoBehaviour
  {
    public GameBootstrapper BootstrapperPrefab;
    private void Awake()
    {
      var bootstrapper = FindObjectOfType<GameBootstrapper>();

      if (bootstrapper == null)
        Instantiate(BootstrapperPrefab);
    }
  }
}