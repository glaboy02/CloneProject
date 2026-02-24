using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public TMP_Dropdown settingsDropdown;
    public void StartFlappyBird()
    {
        SceneManager.LoadScene(1);
    }
    public void StartFlappyBirdGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetFlappyBirdScore();
        }
        SceneManager.LoadScene(2);
    }
    public void RestartFlappyBirdGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetFlappyBirdScore();
        }
        SceneManager.LoadScene(2);
    }
    public void StartPong()
    {
        SceneManager.LoadScene(3);
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetPongScore();
        }
    }

    public void RestartPongGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetPongScore();
        }
        SceneManager.LoadScene(3);
    }
    public void StartSnake()
    {
        SceneManager.LoadScene(4);
    }

    public void StartTicTacToe()
    {
        SceneManager.LoadScene(5);
    }
    public void StartAstroids()
    {
        SceneManager.LoadScene(6);
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

    public void ClearSavedData(int selectedIndex)
    {
        if (settingsDropdown != null) // Safe check
        {
            switch (selectedIndex)
            {
                case 0: // Clear Blank data
                    break;
                case 1: // Clear Flappy Bird data
                    SaveManager.Instance.flappyBirdHighScore = 0;
                    SaveManager.Instance.SaveFlappyBirdHighScore();
                    break;
                case 2: // Clear Pong data
                        // Clear pong data
                    break;
                case 3: // Clear Snake data
                        // Clear snake data
                    break;
                case 4: // Clear All data
                    SaveManager.Instance.flappyBirdHighScore = 0;
                    SaveManager.Instance.SaveFlappyBirdHighScore();
                    break;

            }
        }
    }

}
