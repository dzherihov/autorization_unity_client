using UI.Logic;
using UnityEngine;

public class HideCurtain : MonoBehaviour
{
    void Start()
    {
        if(LoadingCurtain.Instance)
            LoadingCurtain.Instance.Hide();
    }
}
