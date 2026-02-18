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
    public void StartPong()
    {
        SceneManager.LoadScene(2);
    }
    public void StartSnake()
    {
        SceneManager.LoadScene(3);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
