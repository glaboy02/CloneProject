using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI pongPlayer1ScoreText;
    [SerializeField] private TextMeshProUGUI pongPlayer2ScoreText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPlayer1Panel;
    [SerializeField] private GameObject winPlayer2Panel;
    private int scoreFlappyBird;
    private int pongPlayer1Score;
    private int pongPlayer2Score;
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
        pongPlayer1Score = 0;
        pongPlayer2Score = 0;
        UpdateFlappyBirdUI();
        UpdatePongUI();
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


    public void IncreaseFlappyBirdScore()
    {
        scoreFlappyBird++;

        UpdateFlappyBirdUI();
    }
    #endregion


    #region Pong Management
    private void OnSceneLoadedPong(Scene scene, LoadSceneMode mode)
    {
        pongPlayer1ScoreText = GameObject.Find("Player1Score")?.GetComponent<TextMeshProUGUI>();
        pongPlayer2ScoreText = GameObject.Find("Player2Score")?.GetComponent<TextMeshProUGUI>();

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

        UpdatePongUI();
    }

    private void UpdatePongUI()
    {
        if (pongPlayer1ScoreText != null && pongPlayer2ScoreText != null)
        {
            pongPlayer1ScoreText.text = pongPlayer1Score.ToString();
            pongPlayer2ScoreText.text = pongPlayer2Score.ToString();
        }

    }
    public void GameStartPong()
    {
        SetGamePaused(false);
        if (winPlayer1Panel != null && winPlayer2Panel != null)
        {
            winPlayer1Panel.SetActive(false);
            winPlayer2Panel.SetActive(false);
        }
    }

    public void GameOverPong(int winningPlayer)
    {
        SetGamePaused(true);
        Debug.Log("Game Over!");

        if (winningPlayer == 1 && winPlayer1Panel != null)
        {
            winPlayer1Panel.SetActive(true);
        }
        else if (winningPlayer == 2 && winPlayer2Panel != null)
        {
            winPlayer2Panel.SetActive(true);
        }

        ResetPongScore();

        // if (SaveManager.Instance != null)
        // {
        //     if (scoreFlappyBird > SaveManager.Instance.flappyBirdHighScore)
        //     {
        //         SaveManager.Instance.flappyBirdHighScore = scoreFlappyBird;
        //         SaveManager.Instance.SaveFlappyBirdHighScore();
        //     }
        // }
    }
    public void ResetPongScore()
    {
        pongPlayer1Score = 0;
        pongPlayer2Score = 0;
        UpdatePongUI();
    }

    public void IncreasePongScore(string player)
    {
        Debug.Log("IncreasePongScore called for " + player);
        if (player == "LeftLine")
        {
            pongPlayer1Score++;
        }
        else if (player == "RightLine")
        {
            pongPlayer2Score++;
        }

        UpdatePongUI();
    }

    #endregion


}
