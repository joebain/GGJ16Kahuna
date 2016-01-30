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
            if (scene.name != BaseScene && scene.name != FirstScene && scene.isLoaded)
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



        Scene firstScene = SceneManager.GetSceneByName(FirstScene);
        SwitchToCameraInScene(firstScene);
    }

    private void SwitchToCameraInScene(Scene scene)
    {
        foreach (Camera camera in Camera.allCameras)
        {
            camera.enabled = false;
            camera.gameObject.SetActive(false);
        }

        foreach (GameObject root in scene.GetRootGameObjects())
        {
            Camera camera = root.GetComponent<Camera>();
            if (camera != null)
            {
                camera.gameObject.SetActive(true);
                camera.enabled = true;
            }
        }
    }

    public static AreaManager Get()
    {
        return instance;
    }

    public void ChangeArea(string sceneName)
    {
        Debug.Log("change area to " + sceneName);

        Scene newScene = SceneManager.GetSceneByName(sceneName);
        SceneManager.LoadScene(newScene.buildIndex, LoadSceneMode.Additive);
        SwitchToCameraInScene(newScene);

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
