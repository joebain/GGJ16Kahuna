using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaManager : MonoBehaviour
{
    public string BaseScene = "main";
    public string FirstScene = "inside-hut";

    public static AreaManager instance;

    private string currentArea;
    private float lastChangeTime = 0;

    void Start()
    {
        instance = this;

        StartCoroutine("ChangeAreaAsync", FirstScene);
    }

    private void SwitchToCameraInScene(Scene scene)
    {
        foreach (Camera camera in Camera.allCameras)
        {
            camera.enabled = false;
            camera.gameObject.SetActive(false);
        }

        if (scene.isLoaded)
        {
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
    }

    public static AreaManager Get()
    {
        return instance;
    }

    IEnumerator ChangeAreaAsync(string sceneName)
    {
        Debug.Log("change area to " + sceneName);

        currentArea = sceneName;
        lastChangeTime = Time.time;

        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        Scene newScene = SceneManager.GetSceneByName(sceneName);
        if (newScene.buildIndex != -1)
        {
            SwitchToCameraInScene(newScene);
        }

        Scene[] allScenes = SceneManager.GetAllScenes();
        for (int s = 0; s < SceneManager.sceneCount; s++)
        {
            Scene scene = SceneManager.GetSceneAt(s);
            if (scene.name != BaseScene && scene.name != sceneName && scene.isLoaded)
            {
                Debug.Log("unload scene " + scene.name);
                SceneManager.UnloadScene(scene.name);

            }
        }

        yield return null;
    }

    public void ChangeArea(string sceneName)
    {
        if (currentArea == sceneName) return;

        if (Time.time - lastChangeTime < 2) return;

        StartCoroutine("ChangeAreaAsync", sceneName);
    }
}
