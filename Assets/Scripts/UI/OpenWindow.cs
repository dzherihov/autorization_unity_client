using UnityEngine;

public class OpenWindow : MonoBehaviour
{
   [SerializeField] private Transform openPanel;

   [SerializeField] private bool isDestroyed;

   public void OpenPanel()
   {
      if(isDestroyed) Destroy(gameObject);
      Transform panelInst = Instantiate(openPanel, transform.parent.position, Quaternion.identity);
      panelInst.SetParent(transform.parent,false);
   }

   public void OpenCloseModale()
   {
     openPanel.gameObject.SetActive(!openPanel.gameObject.activeSelf);
   }
}
