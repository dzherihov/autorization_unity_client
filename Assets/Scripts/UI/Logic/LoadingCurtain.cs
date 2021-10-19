using System.Collections;
using UnityEngine;

namespace UI.Logic
{
  public class LoadingCurtain : MonoBehaviour
  {
    public static LoadingCurtain Instance;
    
    public CanvasGroup Curtain;

    private void Awake()
    {
      Singleton();
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

    public void Show()
    {
      gameObject.SetActive(true);
      Curtain.alpha = 1;
    }

    public void Hide() => StartCoroutine(FadeIn(Constants.stepAnimationsUI));

    private IEnumerator FadeIn(float step)
    {
      while (Curtain.alpha > 0)
      {
        Curtain.alpha -= step;
        yield return new WaitForFixedUpdate();
      }
      
      gameObject.SetActive(false);
    }
  }
}