using UnityEngine;

public class GameManagerFlappy : MonoBehaviour
{
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
}
