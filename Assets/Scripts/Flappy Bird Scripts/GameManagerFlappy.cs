using UnityEngine;
using TMPro;

public class GameManagerFlappy : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score;
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

        score = 0;
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOver()
    {
        SetGamePaused(true);
        Debug.Log("Game Over!");
    }

    public static bool GameplayPaused { get; private set; }

    public static void SetGamePaused(bool isPaused)
    {
        GameplayPaused = isPaused;
    }

    public void IncreaseScore()
    {
        score++;
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
