using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{

    public bool SetUpgrades(int position, int upgradeNumber)
    {
        for (int i = 1; i <= 3; i++)
        {
            if (PlayerPrefs.GetInt("UPGRADE " + i) == upgradeNumber && upgradeNumber != 0)
            {
                return false;
            }
        }
        PlayerPrefs.SetInt("UPGRADE " + position, upgradeNumber);
        return true;
    }

    public int GetUpgrades(int n)
    {
        return PlayerPrefs.GetInt("UPGRADE " + n);
    }

    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
