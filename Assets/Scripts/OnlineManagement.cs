using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class OnlineManagement : MonoBehaviour
{
    public enum OS { iOS, Android, Unknown };
    OS deviceOS;

    void Awake()
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
#if (!NO_GPGS)
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
#endif
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
        if (PlayerPrefs.GetInt(GPGSIds.leaderboard_high_score, 0) <= -1)
        {
            PushHighScore(GetComponent<PlayerPrefsManager>().GetHighScore());
        }
        if (PlayerPrefs.GetInt(GPGSIds.leaderboard_longest_survival_time, 0) <= -1)
        {
            PushLongestSurvivalTime(GetComponent<PlayerPrefsManager>().GetLongestSurvivalTime());
        }
        if (PlayerPrefs.GetInt(GPGSIds.leaderboard_total_points, 0) <= -1)
        {
            PushTotalPoints();
        }
    }

    public void PushHighScore(int score)
    {
        if (deviceOS == OS.Android)
        {
            Social.ReportScore(score, GPGSIds.leaderboard_high_score, (bool success) =>
            {
                if (success)
                {
                    PlayerPrefs.SetInt(GPGSIds.leaderboard_high_score, 1);
                }
                else
                {
                    PlayerPrefs.SetInt(GPGSIds.leaderboard_high_score, -score);
                }
            });
        }
        else if (deviceOS == OS.iOS)
        {

        }
    }

    public void PushTotalPoints()
    {
        if (deviceOS == OS.Android)
        {
            Social.ReportScore(GetComponent<PlayerPrefsManager>().GetTotalPoints(), GPGSIds.leaderboard_total_points, (bool success) =>
            {
                if (success)
                {
                    PlayerPrefs.SetInt(GPGSIds.leaderboard_total_points, 1);
                }
                else
                {
                    PlayerPrefs.SetInt(GPGSIds.leaderboard_total_points, -1);
                }
            });
        }
        else if (deviceOS == OS.iOS)
        {

        }
    }

    public void PushLongestSurvivalTime(int time)
    {
        if (deviceOS == OS.Android)
        {
            Social.ReportScore(time, GPGSIds.leaderboard_longest_survival_time, (bool success) =>
            {
                if (success)
                {
                    PlayerPrefs.SetInt(GPGSIds.leaderboard_longest_survival_time, 1);
                }
                else
                {
                    PlayerPrefs.SetInt(GPGSIds.leaderboard_longest_survival_time, -time);
                }
            });
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
        if (PlayerPrefs.GetInt(GPGSIds.achievement_flame_broiled, 0) <= -1)
        {
            CheckNormalAchievement(GPGSIds.achievement_flame_broiled);
        }
        if (PlayerPrefs.GetInt(GPGSIds.achievement_golden_glover, 0) <= -1)
        {
            CheckNormalAchievement(GPGSIds.achievement_golden_glover);
        }
        if (PlayerPrefs.GetInt(GPGSIds.achievement_picture_perfect, 0) <= -1)
        {
            CheckNormalAchievement(GPGSIds.achievement_picture_perfect);
        }
        if (PlayerPrefs.GetInt(GPGSIds.achievement_sous_chef, 0) <= -1)
        {
            CheckNormalAchievement(GPGSIds.achievement_sous_chef);
        }
        if (PlayerPrefs.GetInt(GPGSIds.achievement_chef_de_cuisine, 0) <= -1)
        {
            CheckNormalAchievement(GPGSIds.achievement_chef_de_cuisine);
        }
        if (PlayerPrefs.GetInt(GPGSIds.achievement_surviving_the_lunch_rush, 0) <= -1)
        {
            CheckNormalAchievement(GPGSIds.achievement_surviving_the_lunch_rush);
        }
        if (PlayerPrefs.GetInt(GPGSIds.achievement_experienced_chef, 0) <= -1)
        {
            IncrementAchievement(GPGSIds.achievement_experienced_chef, -PlayerPrefs.GetInt(GPGSIds.achievement_experienced_chef, 0));
        }
        if (PlayerPrefs.GetInt(GPGSIds.achievement_satisfied_zombie, 0) <= -1)
        {
            IncrementAchievement(GPGSIds.achievement_satisfied_zombie, -PlayerPrefs.GetInt(GPGSIds.achievement_satisfied_zombie, 0));
        }
        if (PlayerPrefs.GetInt(GPGSIds.achievement_culinary_genius, 0) <= -1)
        {
            IncrementAchievement(GPGSIds.achievement_culinary_genius, -PlayerPrefs.GetInt(GPGSIds.achievement_culinary_genius, 0));
        }
        if (PlayerPrefs.GetInt(GPGSIds.achievement_all_that_power, 0) <= -1)
        {
            IncrementAchievement(GPGSIds.achievement_all_that_power, -PlayerPrefs.GetInt(GPGSIds.achievement_all_that_power, 0));
        }
        if (PlayerPrefs.GetInt(GPGSIds.achievement_paint_job, 0) <= -1)
        {
            IncrementAchievement(GPGSIds.achievement_paint_job, -PlayerPrefs.GetInt(GPGSIds.achievement_paint_job, 0));
        }
        if (PlayerPrefs.GetInt(GPGSIds.achievement_the_floor_below_me, 0) <= -1)
        {
            IncrementAchievement(GPGSIds.achievement_the_floor_below_me, -PlayerPrefs.GetInt(GPGSIds.achievement_the_floor_below_me, 0));
        }
        if (PlayerPrefs.GetInt(GPGSIds.achievement_the_little_things, 0) <= -1)
        {
            IncrementAchievement(GPGSIds.achievement_the_little_things, -PlayerPrefs.GetInt(GPGSIds.achievement_the_little_things, 0));
        }
        if (PlayerPrefs.GetInt(GPGSIds.achievement_a_new_look, 0) <= -1)
        {
            IncrementAchievement(GPGSIds.achievement_a_new_look, -PlayerPrefs.GetInt(GPGSIds.achievement_a_new_look, 0));
        }
    }

    public void BurnedBurger()
    {
        string achievement = "";
        if (deviceOS == OS.Android)
        {
            achievement = GPGSIds.achievement_flame_broiled;
        }
        else if (deviceOS == OS.iOS)
        {

        }
        CheckNormalAchievement(achievement);
    }

    public void FarawayThrow()
    {
        string achievement = "";
        if (deviceOS == OS.Android)
        {
            achievement = GPGSIds.achievement_golden_glover;
        }
        else if (deviceOS == OS.iOS)
        {
            //Social.ReportProgress(GPGSIds.achievement_the_creator, 100.0f, (bool success) => { });
        }
        CheckNormalAchievement(achievement);
    }

    public void PerfectFoodItem()
    {
        string achievement = "";
        if (deviceOS == OS.Android)
        {
            achievement = GPGSIds.achievement_picture_perfect;
        }
        else if (deviceOS == OS.iOS)
        {
            //Social.ReportProgress(GPGSIds.achievement_the_creator, 100.0f, (bool success) => { });
        }
        CheckNormalAchievement(achievement);
    }

    public void CheckScore(int score)
    {
        if (score > 100)
        {
            string achievement = "";
            if (deviceOS == OS.Android)
            {
                achievement = GPGSIds.achievement_sous_chef;
            }
            else if (deviceOS == OS.iOS)
            {
                //Social.ReportProgress(GPGSIds.achievement_the_creator, 100.0f, (bool success) => { });
            }
            CheckNormalAchievement(achievement);
        }
        if (score > 1000)
        {
            string achievement = "";
            if (deviceOS == OS.Android)
            {
                achievement = GPGSIds.achievement_chef_de_cuisine;
            }
            else if (deviceOS == OS.iOS)
            {
                //Social.ReportProgress(GPGSIds.achievement_the_creator, 100.0f, (bool success) => { });
            }
            CheckNormalAchievement(achievement);
        }
    }

    public void CheckTotalPoints(int lifetimePoints, int pointsIncrease)
    {
        int beforeScore = (lifetimePoints - pointsIncrease) % 10;
        int increase = beforeScore + pointsIncrease;
        increase = increase / 10;
        if (increase > 0)
        {
            string achievement = "";
            if (deviceOS == OS.Android)
            {
                achievement = GPGSIds.achievement_experienced_chef;
            }
            else if (deviceOS == OS.iOS)
            {
                //Social.ReportProgress(GPGSIds.achievement_the_creator, lifetimePoints/100000.0f, (bool success) => { });
            }
            CheckIncrementAchievement(achievement, increase, 10000, (lifetimePoints / 10));
        }
    }

    public void CheckTime(int milliseconds)
    {
        if (milliseconds >= 600000)
        {
            string achievement = "";
            if (deviceOS == OS.Android)
            {
                achievement = GPGSIds.achievement_surviving_the_lunch_rush;
            }
            else if (deviceOS == OS.iOS)
            {
                //Social.ReportProgress(GPGSIds.achievement_the_creator, 100.0f, (bool success) => { });
            }
            CheckNormalAchievement(achievement);
        }
    }

    public void CheckCompletedOrders(int amount)
    {
        if (amount > 0)
        {
            string achievement = "";
            int total = GetComponent<PlayerPrefsManager>().GetOrdersCompleted();
            if (deviceOS == OS.Android)
            {
                achievement = GPGSIds.achievement_satisfied_zombie;
            }
            else if (deviceOS == OS.iOS)
            {
                //Social.ReportProgress(GPGSIds.achievement_the_creator, totalOrder/250.0f, (bool success) => { });
            }
            CheckIncrementAchievement(achievement, amount, 250, total);
        }
    }

    public void CheckFoodProductAmount(int amount)
    {
        if (amount > 0)
        {
            string achievement = "";
            int total = GetComponent<PlayerPrefsManager>().GetFoodProduced();
            if (deviceOS == OS.Android)
            {
                achievement = GPGSIds.achievement_culinary_genius;
            }
            else if (deviceOS == OS.iOS)
            {
                //Social.ReportProgress(GPGSIds.achievement_the_creator, total/250.0f, (bool success) => { });
            }
            CheckIncrementAchievement(achievement, amount, 1000, total);
        }
    }

    public void CheckPowerUps(int total)
    {
        string achievement = "";
        if (deviceOS == OS.Android)
        {
            achievement = GPGSIds.achievement_all_that_power;
        }
        else if (deviceOS == OS.iOS)
        {
            //Social.ReportProgress(GPGSIds.achievement_the_creator, total/20.0f, (bool success) => { });
        }
        CheckIncrementAchievement(achievement, 1, 20, total);
    }

    public void CheckWallpaper(int total)
    {
        string achievement = "";
        if (deviceOS == OS.Android)
        {
            achievement = GPGSIds.achievement_paint_job;
        }
        else if (deviceOS == OS.iOS)
        {
            //Social.ReportProgress(GPGSIds.achievement_the_creator, total/20.0f, (bool success) => { });
        }
        CheckIncrementAchievement(achievement, 1, 20, total);
    }

    public void CheckFlooring(int total)
    {
        string achievement = "";
        if (deviceOS == OS.Android)
        {
            achievement = GPGSIds.achievement_the_floor_below_me;
        }
        else if (deviceOS == OS.iOS)
        {
            //Social.ReportProgress(GPGSIds.achievement_the_creator, total/20.0f, (bool success) => { });
        }
        CheckIncrementAchievement(achievement, 1, 20, total);
    }

    public void CheckDetail(int total)
    {
        string achievement = "";
        if (deviceOS == OS.Android)
        {
            achievement = GPGSIds.achievement_the_little_things;
        }
        else if (deviceOS == OS.iOS)
        {
            //Social.ReportProgress(GPGSIds.achievement_the_creator, total/20.0f, (bool success) => { });
        }
        CheckIncrementAchievement(achievement, 1, 20, total);
    }

    public void CheckGraphics(int total)
    {
        string achievement = "";
        if (deviceOS == OS.Android)
        {
            achievement = GPGSIds.achievement_a_new_look;
        }
        else if (deviceOS == OS.iOS)
        {
            //Social.ReportProgress(GPGSIds.achievement_the_creator, total/10.0f, (bool success) => { });
        }
        CheckIncrementAchievement(achievement, 1, 20, total);
    }

    void CheckNormalAchievement(string achievement)
    {
        if (achievement != "")
        {
            if (PlayerPrefs.GetInt(achievement, 0) != 1)
            {
                Social.ReportProgress(achievement, 100.0f, (bool success) =>
                {
                    if (success)
                    {
                        PlayerPrefs.SetInt(achievement, 1);
                    }
                    else
                    {
                        PlayerPrefs.SetInt(achievement, -1);
                    }
                });
            }
        }
    }

    void CheckIncrementAchievement(string achievement, int increment, int max, int total)
    {
        if (achievement != "")
        {
            if (PlayerPrefs.GetInt(achievement, 0) != 1)
            {
#if (!NO_GPGS)
                PlayGamesPlatform.Instance.IncrementAchievement(achievement, increment, (bool success) =>
                {
                    if (success)
                    {
                        PlayerPrefs.SetInt(achievement, 0);
                    }
                    else
                    {
                        PlayerPrefs.SetInt(achievement, PlayerPrefs.GetInt(achievement, 0) - increment);
                    }
                });
#endif
                if (total >= max)
                {
                    Social.ReportProgress(achievement, 100.0f, (bool success) =>
                    {
                        if (success)
                        {
                            PlayerPrefs.SetInt(achievement, 1);
                        }
                        else
                        {
                            PlayerPrefs.SetInt(achievement, -max);
                        }
                    });
                }
            }
        }
    }

    void IncrementAchievement(string achievement, int amount)
    {
        if (deviceOS == OS.Android)
        {
#if (!NO_GPGS)
            PlayGamesPlatform.Instance.IncrementAchievement(achievement, amount, (bool success) =>
            {
                if (success)
                {
                    PlayerPrefs.SetInt(achievement, 0);
                }
            });
#endif
        }
        else if (deviceOS == OS.iOS)
        {
            //Social.ReportProgress(GPGSIds.achievement_the_creator, total/10.0f, (bool success) => { });
        }
    }
}
