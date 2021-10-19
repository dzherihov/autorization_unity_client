using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetNotifyMessage : MonoBehaviour
{
  [SerializeField] private TMP_Text messageText;
  [SerializeField] private LoadingModale loadModScr;
  [SerializeField] private float lifeTime = 2;
  [SerializeField] private Color errorColor;
  [SerializeField] private Color accessColor;
  [SerializeField] private Image panel;
  public enum TypesNotify {Error, Access}

  public TypesNotify TypeDeposit{
    set
    {
      switch (value)
      {
        case TypesNotify.Access:
          panel.color = accessColor;
          break;
        case TypesNotify.Error:
          panel.color = errorColor;
          break;
        default:
          panel.color = accessColor;
          break;
      }
    }
  }

  public string Message
  {
    set => messageText.text = value;
  }

  private void Start()
  {
    StartCoroutine(LifeTimeWindow(lifeTime));
  }

  private IEnumerator LifeTimeWindow(float time)
  {
    yield return new WaitForSeconds(time);
    loadModScr.Hide();
  }
}
