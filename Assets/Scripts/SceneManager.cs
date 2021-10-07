using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public string bestPlayer { get; private set; }
    public int bestScore { get; private set; }
    public string currPlayer { get; private set; }
    public int currScore { get; private set; }
    public static DataManager Instance { get; private set; }
    private string path;

    void Awake()
    {
        path = Application.persistentDataPath + "/savefile.json";

        if (DataManager.Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        setInstance(this);
        DontDestroyOnLoad(gameObject);
        LoadBestScore();
    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int score;
    }

    public void SaveBestScore(int score)
    {
        SaveData data = new SaveData();
        if (score > bestScore)
        {
            data.playerName = currPlayer;
            data.score = score;
            setBestPlayer(currPlayer);
            setBestScore(score);

            string json = JsonUtility.ToJson(data);

            File.WriteAllText(path, json);
        }
    }

    public void LoadBestScore()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            setBestPlayer(data.playerName);
            setBestScore(data.score);
            setCurrPlayer(data.playerName);
        }
    }

    public void setBestPlayer(string name)
    {
        this.bestPlayer = name;
    }

    public void setBestScore(int score)
    {
        this.bestScore = score;
    }

    public void setCurrPlayer(string name)
    {
        this.currPlayer = name;
    }

    public void setCurrScore(int score)
    {
        this.currScore = score;
    }

    public void setInstance(DataManager instance)
    {
        DataManager.Instance = instance;
    }
}
