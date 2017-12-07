﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    public bool SetUpgrades(int position, int upgradeNumber)
    {
        for (int i = 1; i <= 3; i++)
        {
            if (PlayerPrefs.GetInt("UPGRADE " + i, -1) == upgradeNumber && upgradeNumber != -1)
            {
                return false;
            }
        }
        PlayerPrefs.SetInt("UPGRADE " + position, upgradeNumber);
        return true;
    }

    public bool SlotContainsUpgrade(int pos, int upgradeNumber)
    {
        if (PlayerPrefs.GetInt("UPGRADE " + pos, -1) == upgradeNumber)
        {
            return true;
        }
        return false;
    }

    public int WhichSlotContainsUpgrade(int upgradeNumber)
    {
        for (int i = 1; i <= 3; i++)
        {
            if (PlayerPrefs.GetInt("UPGRADE " + i, -1) == upgradeNumber && upgradeNumber != -1)
            {
                return i;
            }
        }
        return -1;
    }

    public int GetUpgrades(int n)
    {
        return PlayerPrefs.GetInt("UPGRADE " + n, -1);
    }

    public bool ContainsUpgradeBesidesSlot(int n, int slot)
    {
        for (int i = 1; i <= 3; i++)
        {
            if (i != slot)
            {
                if (PlayerPrefs.GetInt("UPGRADE " + i, -1) == n)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool ContainsUpgrade(int n)
    {
        for (int i = 1; i <= 3; i++)
        {
            if (PlayerPrefs.GetInt("UPGRADE " + i, -1) == n)
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
