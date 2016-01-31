using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("main");
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("credits");
    }

    public void GoToSplash()
    {
        SceneManager.LoadScene("splash");
    }
}
