using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public void StartFlappyBird()
    {
        SceneManager.LoadScene(1);
    }
    public void StartFlappyBirdGame()
    {
        SceneManager.LoadScene(2);
    }
    public void RestartFlappyBirdGame()
    {
        SceneManager.LoadScene(2);
    }
    public void StartPong()
    {
        SceneManager.LoadScene(3);
    }
    public void StartSnake()
    {
        SceneManager.LoadScene(4);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#elif UNITY_WEBGL
        // WebGL does not support quitting the application, so we can redirect to a different page or simply do nothing.
        return;
#else
        Application.Quit();
#endif
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
