using System.Collections;
using UnityEngine;

public class SwitchPanels : MonoBehaviour
{
   [SerializeField] private Transform Panel1;
   [SerializeField] private Transform Panel2;

   public void Switch()
   {
      if (Panel1.gameObject.activeSelf)
      {
         StartCoroutine(FadeIn(Panel1, Constants.stepAnimationsUI));
         StartCoroutine(FadeOut(Panel2, Constants.stepAnimationsUI));
      }
      else
      {
         StartCoroutine(FadeIn(Panel2, Constants.stepAnimationsUI));
         StartCoroutine(FadeOut(Panel1, Constants.stepAnimationsUI));
      }
   }
   
   
   private IEnumerator FadeIn(Transform window, float step)
   {
      CanvasGroup window_alpha = window.GetComponent<CanvasGroup>();
      while (window_alpha.alpha > 0)
      {
         window_alpha.alpha -= step;
         yield return new WaitForFixedUpdate();
      }
      window.gameObject.SetActive(false);
   }
    
   private IEnumerator FadeOut(Transform window, float step)
   {
      window.gameObject.SetActive(true);
      CanvasGroup window_alpha = window.GetComponent<CanvasGroup>();
      window_alpha.alpha = 0;
      while (window_alpha.alpha < 1)
      {
         window_alpha.alpha += step;
         yield return new WaitForFixedUpdate();
      }
   }
}
