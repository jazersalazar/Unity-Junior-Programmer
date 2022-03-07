using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour 
{
    public static GameManager Instance;

    public string PlayerName;
    public string HighScoreName;
    public int HighScore;

    private void Awake() 
    {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    public class LeaderboardData
    {
        [System.Serializable]
        public class LeaderboardList
        {
            public string LeaderName;
            public int LeaderScore;
        }

        public List<LeaderboardList> Leaderboard;
    }

    public void CheckHighscore(int score)
    {
        if (score > HighScore)
        {
            HighScoreName = PlayerName;
            HighScore = score;
            SaveLeaderboard();
        }
    }

    public void SaveLeaderboard()
    {
        LeaderboardData leaderboard = LoadLeaderboard();

        // Initialize Leaderboard list if no data found
        if (leaderboard == null)
        {
            leaderboard = new LeaderboardData()
            {
                Leaderboard = new List<LeaderboardData.LeaderboardList>()
            };
        }

        // Add new leader on the top of the board
        leaderboard.Leaderboard.Insert(0, new LeaderboardData.LeaderboardList()
        {
            LeaderName = PlayerName,
            LeaderScore = HighScore
        });

        string json = JsonUtility.ToJson(leaderboard);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public LeaderboardData LoadLeaderboard()
    {
        LeaderboardData leaderboard = new LeaderboardData();
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            leaderboard = JsonUtility.FromJson<LeaderboardData>(json);
        }

        return leaderboard;
    }

    public void GetHighScore()
    {
        LeaderboardData leaderboard = LoadLeaderboard();

        // Get Highscore if exist
        if (leaderboard != null)
        {
            HighScoreName = leaderboard.Leaderboard[0].LeaderName;
            HighScore = leaderboard.Leaderboard[0].LeaderScore;
        }
    }
}
