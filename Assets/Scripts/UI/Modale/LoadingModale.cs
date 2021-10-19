using System.Collections;
using UnityEngine;

public class LoadingModale : MonoBehaviour
{
    public CanvasGroup Modale;

    private void Start()
    {
        Show();
    }

    public void Show() => StartCoroutine(FadeOut(Constants.stepAnimationsUI));

    public void Hide() => StartCoroutine(FadeIn(Constants.stepAnimationsUI));

    private IEnumerator FadeIn(float step)
    {
        while (Modale.alpha > 0)
        {
            Modale.alpha -= step;
            yield return new WaitForFixedUpdate();
        }
      
        Destroy(gameObject);
    }
    
    private IEnumerator FadeOut(float step)
    {
        Modale.alpha = 0;
        while (Modale.alpha < 1)
        {
            Modale.alpha += step;
            yield return new WaitForFixedUpdate();
        }

    }
}
