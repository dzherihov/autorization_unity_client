using UnityEngine;

public class OpenModale : MonoBehaviour
{
    [SerializeField] private GameObject modalePrefabe;
    [SerializeField] private Transform parent;
    [SerializeField] private bool useMainCanvasParent;
    [SerializeField] private bool containParent;

    private GameObject modale;

    public void OpenWindow()
    {
        if (containParent)
        {
            if (useMainCanvasParent || !parent)
                parent = GameObject.Find("Canvas").transform;
            modale = Instantiate(modalePrefabe, parent.position, Quaternion.identity);
            modale.transform.SetParent(parent, false);
            modale.transform.localScale = new Vector3(1f, 1f);
        }
        else
        {
            modale = Instantiate(modalePrefabe, modalePrefabe.transform.position, Quaternion.identity);
        }
    }
}
