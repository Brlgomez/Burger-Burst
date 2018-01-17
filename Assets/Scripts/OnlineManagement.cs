using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class OnlineManagement : MonoBehaviour
{
    public enum OS { iOS, Android, Unknown };
    OS deviceOS;

    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            deviceOS = OS.Android;
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            deviceOS = OS.iOS;
        }
        else
        {
            deviceOS = OS.Unknown;
        }
        if (deviceOS == OS.Android)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
        }
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
        if (deviceOS == OS.Android)
        {
            Social.ReportScore(score, GPGSIds.leaderboard_high_score, (bool success) => { });
        }
        else if (deviceOS == OS.iOS)
        {

        }
    }

    public void PushTotalPoints()
    {
        if (deviceOS == OS.Android)
        {
            Social.ReportScore(GetComponent<PlayerPrefsManager>().GetTotalPoints(), GPGSIds.leaderboard_total_points, (bool success) => { });
        }
        else if (deviceOS == OS.iOS)
        {

        }
    }

    public void PushLongestSurvivalTime(int time)
    {
        if (deviceOS == OS.Android)
        {
            Social.ReportScore(time, GPGSIds.leaderboard_longest_survival_time, (bool success) => { });
        }
        else if (deviceOS == OS.iOS)
        {

        }
    }

    /* 
     * 
     * 
     * ACHIEVEMENTS
     * 
     * 
    */

    public void GetAchievements()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Social.ShowAchievementsUI();
            }
        });
    }

    public void CheckAllAchievements()
    {
        //TODO: Use player prefs to check to see whcih achievements were unlocked with an error
    }

    public void BurnedBurger()
    {
        if (deviceOS == OS.Android)
        {
            //Social.ReportProgress(GPGSIds.achievement_the_creator, 100.0f, (bool success) => { });
        }
        else if (deviceOS == OS.iOS)
        {
            //Social.ReportProgress(GPGSIds.achievement_the_creator, 100.0f, (bool success) => { });
        }
    }

    public void CheckScore(int score)
    {
        if (score >= 100)
        {
            if (deviceOS == OS.Android)
            {
                //Social.ReportProgress(GPGSIds.achievement_the_creator, 100.0f, (bool success) => { });
            }
            else if (deviceOS == OS.iOS)
            {
                //Social.ReportProgress(GPGSIds.achievement_the_creator, 100.0f, (bool success) => { });
            }
        }
        if (score >= 1000)
        {
            if (deviceOS == OS.Android)
            {
                //Social.ReportProgress(GPGSIds.achievement_the_creator, 100.0f, (bool success) => { });
            }
            else if (deviceOS == OS.iOS)
            {
                //Social.ReportProgress(GPGSIds.achievement_the_creator, 100.0f, (bool success) => { });
            }
        }
    }

    public void CheckTotalPoints(int lifetimePoints, int pointsIncrease)
    {
        if (pointsIncrease > 0)
        {
            if (deviceOS == OS.Android)
            {
                //PlayGamesPlatform.Instance.IncrementAchievement("Cfjewijawiu_QA", pointsIncrease, (bool success) => { });
                if (lifetimePoints >= 100000)
                {
                    //Social.ReportProgress(GPGSIds.achievement_the_creator, 100.0f, (bool success) => { });
                }
            }
            else if (deviceOS == OS.iOS)
            {
                //Social.ReportProgress(GPGSIds.achievement_the_creator, lifetimePoints/100000.0f, (bool success) => { });
            }
        }
    }

    public void CheckTime(int milliseconds)
    {
        if (milliseconds >= 600000)
        {
            if (deviceOS == OS.Android)
            {
                //Social.ReportProgress(GPGSIds.achievement_the_creator, 100.0f, (bool success) => { });
            }
            else if (deviceOS == OS.iOS)
            {
                //Social.ReportProgress(GPGSIds.achievement_the_creator, 100.0f, (bool success) => { });
            }
        }
    }

    public void CheckCompletedOrders(int amount)
    {
        int total = GetComponent<PlayerPrefsManager>().GetOrdersCompleted();
        if (deviceOS == OS.Android)
        {
            //PlayGamesPlatform.Instance.IncrementAchievement("Cfjewijawiu_QA", amount, (bool success) => { });
            if (total >= 250)
            {
                //Social.ReportProgress(GPGSIds.achievement_the_creator, 100.0f, (bool success) => { });
            }
        }
        else if (deviceOS == OS.iOS)
        {
            //Social.ReportProgress(GPGSIds.achievement_the_creator, totalOrder/250.0f, (bool success) => { });
        }
    }

    public void CheckFoodProductAmount(int amount)
    {
        int total = GetComponent<PlayerPrefsManager>().GetFoodProduced();
        if (deviceOS == OS.Android)
        {
            //PlayGamesPlatform.Instance.IncrementAchievement("Cfjewijawiu_QA", amount, (bool success) => { });
            if (total >= 1000)
            {
                //Social.ReportProgress(GPGSIds.achievement_the_creator, 100.0f, (bool success) => { });
            }
        }
        else if (deviceOS == OS.iOS)
        {
            //Social.ReportProgress(GPGSIds.achievement_the_creator, total/250.0f, (bool success) => { });
        }
    }

    public void CheckPowerUps(int total)
    {
        if (deviceOS == OS.Android)
        {
            //PlayGamesPlatform.Instance.IncrementAchievement("Cfjewijawiu_QA", 1, (bool success) => { });
            if (total >= 20)
            {
                //Social.ReportProgress(GPGSIds.achievement_the_creator, 100.0f, (bool success) => { });
            }
        }
        else if (deviceOS == OS.iOS)
        {
            //Social.ReportProgress(GPGSIds.achievement_the_creator, total/20.0f, (bool success) => { });
        }
    }

    public void CheckWallpaper(int total)
    {
        if (deviceOS == OS.Android)
        {
            //PlayGamesPlatform.Instance.IncrementAchievement("Cfjewijawiu_QA", 1, (bool success) => { });
            if (total >= 20)
            {
                //Social.ReportProgress(GPGSIds.achievement_the_creator, 100.0f, (bool success) => { });
            }
        }
        else if (deviceOS == OS.iOS)
        {
            //Social.ReportProgress(GPGSIds.achievement_the_creator, total/20.0f, (bool success) => { });
        }
    }

    public void CheckFlooring(int total)
    {
        if (deviceOS == OS.Android)
        {
            //PlayGamesPlatform.Instance.IncrementAchievement("Cfjewijawiu_QA", 1, (bool success) => { });
            if (total >= 20)
            {
                //Social.ReportProgress(GPGSIds.achievement_the_creator, 100.0f, (bool success) => { });
            }
        }
        else if (deviceOS == OS.iOS)
        {
            //Social.ReportProgress(GPGSIds.achievement_the_creator, total/20.0f, (bool success) => { });
        }
    }

    public void CheckDetail(int total)
    {
        if (deviceOS == OS.Android)
        {
            //PlayGamesPlatform.Instance.IncrementAchievement("Cfjewijawiu_QA", 1, (bool success) => { });
            if (total >= 20)
            {
                //Social.ReportProgress(GPGSIds.achievement_the_creator, 100.0f, (bool success) => { });
            }
        }
        else if (deviceOS == OS.iOS)
        {
            //Social.ReportProgress(GPGSIds.achievement_the_creator, total/20.0f, (bool success) => { });
        }
    }

    public void CheckGraphics(int total)
    {
        if (deviceOS == OS.Android)
        {
            //PlayGamesPlatform.Instance.IncrementAchievement("Cfjewijawiu_QA", 1, (bool success) => { });
            if (total >= 10)
            {
                //Social.ReportProgress(GPGSIds.achievement_the_creator, 100.0f, (bool success) => { });
            }
        }
        else if (deviceOS == OS.iOS)
        {
            //Social.ReportProgress(GPGSIds.achievement_the_creator, total/10.0f, (bool success) => { });
        }
    }
}