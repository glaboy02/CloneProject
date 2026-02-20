using UnityEngine;
using System.IO;
using System;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    // public int longestRun;
    // public int currentRun;
    public int flappyBirdHighScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadFlappyBirdHighScore();
    }

    [Serializable]
    class SaveData
    {
        public int flappyBirdHighScore;
    }

    #region Flappy Bird High Score Save/Load
    public void SaveFlappyBirdHighScore()
    {
        SaveData data = new SaveData();
        data.flappyBirdHighScore = flappyBirdHighScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/flappybirdsave.json", json);
    }

    public void LoadFlappyBirdHighScore()
    {
        string path = Application.persistentDataPath + "/flappybirdsave.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            flappyBirdHighScore = data.flappyBirdHighScore;
        }
    }
    #endregion


}