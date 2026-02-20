using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManagerFlappy : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    private int scoreFlappyBird;
    public static GameManagerFlappy Instance { get; private set; }

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
        UpdateUI();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        scoreText = GameObject.Find("FlappyScore")?.GetComponent<TextMeshProUGUI>();
        highScoreText = GameObject.Find("FlappyHighscore")?.GetComponent<TextMeshProUGUI>();
        UpdateUI();
    }

    private void UpdateUI()
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

    public void GameOver()
    {
        SetGamePaused(true);
        Debug.Log("Game Over!");
        if (SaveManager.Instance != null)
        {
            if (scoreFlappyBird > SaveManager.Instance.flappyBirdHighScore)
            {
                SaveManager.Instance.flappyBirdHighScore = scoreFlappyBird;
                SaveManager.Instance.SaveFlappyBirdHighScore();
            }
        }
    }

    public static bool GameplayPaused { get; private set; }

    public static void SetGamePaused(bool isPaused)
    {
        GameplayPaused = isPaused;
    }

    public void ResetScore()
    {
        scoreFlappyBird = 0;
        UpdateUI();
    }

    public void IncreaseScore()
    {
        scoreFlappyBird++;
        UpdateUI();
    }
}
