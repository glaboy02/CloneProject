using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private GameObject gameOverPanel;
    private int scoreFlappyBird;
    // private int pongPlayer1Score;
    // private int pongPlayer2Score;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        scoreFlappyBird = 0;
        UpdateFlappyBirdUI();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoadedFlappyBird;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoadedFlappyBird;
    }
    public static bool GameplayPaused { get; private set; }

    public static void SetGamePaused(bool isPaused)
    {
        GameplayPaused = isPaused;
    }

    #region Flappy Bird Management
    private void OnSceneLoadedFlappyBird(Scene scene, LoadSceneMode mode)
    {
        scoreText = GameObject.Find("FlappyScore")?.GetComponent<TextMeshProUGUI>();
        highScoreText = GameObject.Find("FlappyHighscore")?.GetComponent<TextMeshProUGUI>();

        // Search through root GameObjects to find Game Over Canvas (works even if inactive)
        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject root in rootObjects)
        {
            if (root.name == "Game Over Canvas")
            {
                gameOverPanel = root;
                break;
            }
        }

        UpdateFlappyBirdUI();
    }

    private void UpdateFlappyBirdUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + scoreFlappyBird;
        }

        if (highScoreText != null && SaveManager.Instance != null)
        {
            highScoreText.text = "High Score: " + SaveManager.Instance.flappyBirdHighScore;
        }
    }
    public void GameStartFlappyBird()
    {
        SetGamePaused(false);
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void GameOverFlappyBird()
    {
        SetGamePaused(true);
        Debug.Log("Game Over!");
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (SaveManager.Instance != null)
        {
            if (scoreFlappyBird > SaveManager.Instance.flappyBirdHighScore)
            {
                SaveManager.Instance.flappyBirdHighScore = scoreFlappyBird;
                SaveManager.Instance.SaveFlappyBirdHighScore();
            }
        }
    }
    public void ResetFlappyBirdScore()
    {
        scoreFlappyBird = 0;

        UpdateFlappyBirdUI();
    }
    // public void ResetPongScore()
    // {
    //     pongPlayer1Score = 0;
    //     pongPlayer2Score = 0;
    //     UpdateFlappyBirdUI();
    // }

    public void IncreaseFlappyBirdScore()
    {
        scoreFlappyBird++;

        UpdateFlappyBirdUI();
    }
    #endregion


    
}
