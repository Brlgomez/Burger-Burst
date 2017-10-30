using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    public bool SetUpgrades(int position, int upgradeNumber)
    {
        for (int i = 1; i <= 3; i++)
        {
            if (PlayerPrefs.GetInt("UPGRADE " + i) == upgradeNumber && upgradeNumber != PowerUpsManager.nothing)
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

    public bool ContainsUpgrade(int n)
    {
		for (int i = 1; i <= 3; i++)
		{
            if (PlayerPrefs.GetInt("UPGRADE " + i) == n)
            {
                return true;
            }
		}
        return false;
    }

    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
