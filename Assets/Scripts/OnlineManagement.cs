using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if (NO_GPGS)
using UnityEngine.SocialPlatforms.GameCenter;
#endif
#if (!NO_GPGS)
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#endif

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
#if (NO_GPGS)
			GameCenterPlatform.ShowDefaultAchievementCompletionBanner(true);
#endif
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
        if (deviceOS == OS.iOS || deviceOS == OS.Android)
        {
            LogIn();
        }
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
        if (deviceOS == OS.Android)
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
        else if (deviceOS == OS.iOS)
        {
            if (PlayerPrefs.GetInt(GCIds.leaderboard_high_score, 0) <= -1)
            {
                PushHighScore(GetComponent<PlayerPrefsManager>().GetHighScore());
            }
            if (PlayerPrefs.GetInt(GCIds.leaderboard_longest_survival_time, 0) <= -1)
            {
                PushLongestSurvivalTime(GetComponent<PlayerPrefsManager>().GetLongestSurvivalTime());
            }
            if (PlayerPrefs.GetInt(GCIds.leaderboard_total_points, 0) <= -1)
            {
                PushTotalPoints();
            }
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
            Social.ReportScore(score, GCIds.leaderboard_high_score, (bool success) =>
            {
                if (success)
                {
                    PlayerPrefs.SetInt(GCIds.leaderboard_high_score, 1);
                }
                else
                {
                    PlayerPrefs.SetInt(GCIds.leaderboard_high_score, -score);
                }
            });
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
            Social.ReportScore(GetComponent<PlayerPrefsManager>().GetTotalPoints(), GCIds.leaderboard_total_points, (bool success) =>
            {
                if (success)
                {
                    PlayerPrefs.SetInt(GCIds.leaderboard_total_points, 1);
                }
                else
                {
                    PlayerPrefs.SetInt(GCIds.leaderboard_total_points, -1);
                }
            });
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
            Social.ReportScore(time, GCIds.leaderboard_longest_survival_time, (bool success) =>
            {
                if (success)
                {
                    PlayerPrefs.SetInt(GCIds.leaderboard_longest_survival_time, 1);
                }
                else
                {
                    PlayerPrefs.SetInt(GCIds.leaderboard_longest_survival_time, -time);
                }
            });
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
        if (deviceOS == OS.Android)
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
        else if (deviceOS == OS.iOS)
        {
            if (PlayerPrefs.GetInt(GCIds.achievement_flame_broiled, 0) <= -1)
            {
                CheckNormalAchievement(GCIds.achievement_flame_broiled);
            }
            if (PlayerPrefs.GetInt(GCIds.achievement_golden_glover, 0) <= -1)
            {
                CheckNormalAchievement(GCIds.achievement_golden_glover);
            }
            if (PlayerPrefs.GetInt(GCIds.achievement_picture_perfect, 0) <= -1)
            {
                CheckNormalAchievement(GCIds.achievement_picture_perfect);
            }
            if (PlayerPrefs.GetInt(GCIds.achievement_sous_chef, 0) <= -1)
            {
                CheckNormalAchievement(GCIds.achievement_sous_chef);
            }
            if (PlayerPrefs.GetInt(GCIds.achievement_chef_de_cuisine, 0) <= -1)
            {
                CheckNormalAchievement(GCIds.achievement_chef_de_cuisine);
            }
            if (PlayerPrefs.GetInt(GCIds.achievement_surviving_the_lunch_rush, 0) <= -1)
            {
                CheckNormalAchievement(GCIds.achievement_surviving_the_lunch_rush);
            }
            if (PlayerPrefs.GetInt(GPGSIds.achievement_experienced_chef, 0) <= -1)
            {
                CheckNormalAchievement(GCIds.achievement_experienced_chef);
            }
            if (PlayerPrefs.GetInt(GCIds.achievement_satisfied_zombie, 0) <= -1)
            {
                CheckNormalAchievement(GCIds.achievement_satisfied_zombie);
            }
            if (PlayerPrefs.GetInt(GCIds.achievement_culinary_genius, 0) <= -1)
            {
                CheckNormalAchievement(GCIds.achievement_culinary_genius);
            }
            if (PlayerPrefs.GetInt(GCIds.achievement_all_that_power, 0) <= -1)
            {
                CheckNormalAchievement(GCIds.achievement_all_that_power);
            }
            if (PlayerPrefs.GetInt(GCIds.achievement_paint_job, 0) <= -1)
            {
                CheckNormalAchievement(GCIds.achievement_paint_job);
            }
            if (PlayerPrefs.GetInt(GCIds.achievement_the_floor_below_me, 0) <= -1)
            {
                CheckNormalAchievement(GCIds.achievement_the_floor_below_me);
            }
            if (PlayerPrefs.GetInt(GCIds.achievement_the_little_things, 0) <= -1)
            {
                CheckNormalAchievement(GCIds.achievement_the_little_things);
            }
            if (PlayerPrefs.GetInt(GCIds.achievement_a_new_look, 0) <= -1)
            {
                CheckNormalAchievement(GCIds.achievement_a_new_look);
            }
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
            achievement = GCIds.achievement_flame_broiled;
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
            achievement = GCIds.achievement_golden_glover;
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
            achievement = GCIds.achievement_picture_perfect;
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
                achievement = GCIds.achievement_sous_chef;
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
                achievement = GCIds.achievement_chef_de_cuisine;
            }
            CheckNormalAchievement(achievement);
        }
    }

    public void CheckTotalPoints(int lifetimePoints, int pointsIncrease)
    {
        string achievement = "";
        int beforeScore = (lifetimePoints - pointsIncrease) % 10;
        int increase = beforeScore + pointsIncrease;
        increase = increase / 10;
        if (increase > 0)
        {
            if (deviceOS == OS.Android)
            {
                achievement = GPGSIds.achievement_experienced_chef;
                CheckIncrementAchievement(achievement, increase, 10000, (lifetimePoints / 10));
            }
        }
        if (lifetimePoints > 100000 && deviceOS == OS.iOS)
        {
            achievement = GCIds.achievement_experienced_chef;
            CheckNormalAchievement(achievement);
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
                achievement = GCIds.achievement_surviving_the_lunch_rush;
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
                CheckIncrementAchievement(achievement, amount, 250, total);
            }
            else if (deviceOS == OS.iOS && total >= 250)
            {
                achievement = GCIds.achievement_satisfied_zombie;
                CheckNormalAchievement(achievement);
            }
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
                CheckIncrementAchievement(achievement, amount, 1000, total);
            }
            else if (deviceOS == OS.iOS && total >= 1000)
            {
                achievement = GCIds.achievement_culinary_genius;
                CheckNormalAchievement(achievement);
            }
        }
    }

    public void CheckPowerUps(int total)
    {
        string achievement = "";
        if (deviceOS == OS.Android)
        {
            achievement = GPGSIds.achievement_all_that_power;
            CheckIncrementAchievement(achievement, 1, 20, total);
        }
        else if (deviceOS == OS.iOS && total >= 20)
        {
            achievement = GCIds.achievement_all_that_power;
            CheckNormalAchievement(achievement);
        }
    }

    public void CheckWallpaper(int total)
    {
        string achievement = "";
        if (deviceOS == OS.Android)
        {
            achievement = GPGSIds.achievement_paint_job;
            CheckIncrementAchievement(achievement, 1, 20, total);
        }
        else if (deviceOS == OS.iOS && total >= 20)
        {
            achievement = GCIds.achievement_paint_job;
            CheckNormalAchievement(achievement);
        }
    }

    public void CheckFlooring(int total)
    {
        string achievement = "";
        if (deviceOS == OS.Android)
        {
            achievement = GPGSIds.achievement_the_floor_below_me;
            CheckIncrementAchievement(achievement, 1, 20, total);
        }
        else if (deviceOS == OS.iOS && total >= 20)
        {
            achievement = GCIds.achievement_the_floor_below_me;
            CheckNormalAchievement(achievement);
        }
    }

    public void CheckDetail(int total)
    {
        string achievement = "";
        if (deviceOS == OS.Android)
        {
            achievement = GPGSIds.achievement_the_little_things;
            CheckIncrementAchievement(achievement, 1, 20, total);
        }
        else if (deviceOS == OS.iOS && total >= 20)
        {
            achievement = GCIds.achievement_the_little_things;
            CheckNormalAchievement(achievement);
        }
    }

    public void CheckGraphics(int total)
    {
        string achievement = "";
        if (deviceOS == OS.Android)
        {
            achievement = GPGSIds.achievement_a_new_look;
            CheckIncrementAchievement(achievement, 1, 10, total);
        }
        else if (deviceOS == OS.iOS && total >= 10)
        {
            achievement = GCIds.achievement_a_new_look;
            CheckNormalAchievement(achievement);
        }
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
    }
}
