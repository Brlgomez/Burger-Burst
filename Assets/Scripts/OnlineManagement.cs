using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class OnlineManagement : MonoBehaviour
{
    public enum OS { iOS, Android };
    OS deviceOS;

    void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        LogIn();
    }

    public void LogIn()
    {
        Social.localUser.Authenticate((bool success) => { });
    }

    /* 
     * 
     * 
     * LEADERBOARDS 
     * 
     * 
    */

    public void GetLeaderboards()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                PushAllLeaderboards();
                Social.ShowLeaderboardUI();
            }
        });
    }

    public void PushAllLeaderboards()
    {
        PushHighScore(GetComponent<PlayerPrefsManager>().GetHighScore());
        PushTotalPoints();
        PushLongestSurvivalTime(GetComponent<PlayerPrefsManager>().GetLongestSurvivalTime());
    }

    public void PushHighScore(int score)
    {
        Social.ReportScore(score, GPGSIds.leaderboard_high_score, (bool success) => { });
    }

    public void PushTotalPoints()
    {
        Social.ReportScore(GetComponent<PlayerPrefsManager>().GetTotalPoints(), GPGSIds.leaderboard_total_points, (bool success) => { });
    }

    public void PushLongestSurvivalTime(int time)
    {
        Social.ReportScore(time, GPGSIds.leaderboard_longest_survival_time, (bool success) => { });
    }
}