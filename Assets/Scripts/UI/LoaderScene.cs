using Infrastructure;
using RequestBuilderAPI;
using SimpleJSON;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderScene : MonoBehaviour
{
    public void Load(string name)
    {
        var bootstrapper = FindObjectOfType<GameBootstrapper>();
        if(bootstrapper)
            Destroy(bootstrapper.gameObject);
        PlayerPrefs.SetString(Constants.LevelNameKey, name);
        SceneManager.LoadScene(name);
    }
    
    public void LoadWithoutSave(string name)
    {
        var bootstrapper = FindObjectOfType<GameBootstrapper>();
        if(bootstrapper)
            Destroy(bootstrapper.gameObject);
        SceneManager.LoadScene(name);
    }
}
