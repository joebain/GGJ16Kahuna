using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaManager : MonoBehaviour
{
    public string BaseScene = "main";
    public string FirstScene = "inside-hut";

    public static AreaManager instance;

    void Start()
    {
        instance = this;

        Scene[] allScenes = SceneManager.GetAllScenes();
        foreach (Scene scene in allScenes)
        {
            if (scene.name != BaseScene && scene.isLoaded)
            {
                Debug.Log("unloading scene " + scene.name);
                bool wasUnloaded = SceneManager.UnloadScene(scene.buildIndex);
                Debug.Log("success? " + wasUnloaded);
            }
        }

        if (!SceneManager.GetSceneByName(FirstScene).isLoaded)
        {
            Debug.Log("loading first scene " + FirstScene);
           // SceneManager.LoadScene(FirstScene, LoadSceneMode.Additive);
        }

    }

    public static AreaManager Get()
    {
        return instance;
    }

    public void ChangeArea(string sceneName)
    {
        Debug.Log("change area to " + sceneName);

        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

        Scene[] allScenes = SceneManager.GetAllScenes();
        foreach (Scene scene in allScenes)
        {
            if (scene.name != BaseScene && scene.name != sceneName && scene.isLoaded)
            {
                SceneManager.UnloadScene(scene.buildIndex);
            }
        }
    }
}
