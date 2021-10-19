using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenNotify : MonoBehaviour
{
    [SerializeField] private GameObject notifyWindow;
    [SerializeField] private Transform rootParent;
    [SerializeField] private bool useMainCanvasParent = false;

    public void Notify(string message, SetNotifyMessage.TypesNotify type)
    {
        if (useMainCanvasParent || !rootParent)
            rootParent = GameObject.Find("Canvas").transform;
        GameObject modale = Instantiate(notifyWindow, rootParent.position, Quaternion.identity);
        modale.transform.SetParent(rootParent, false);
        modale.transform.localScale = new Vector3(1f, 1f);
        modale.GetComponent<SetNotifyMessage>().Message = message;
        modale.GetComponent<SetNotifyMessage>().TypeDeposit = type;
    }
}
