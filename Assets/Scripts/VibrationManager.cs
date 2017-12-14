using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VibrationManager : MonoBehaviour
{
    bool canVibrate = true;

    void Start()
    {
        canVibrate = GetComponent<PlayerPrefsManager>().GetVibration();
    }

    public void LightTapticFeeddback()
    {
        if (canVibrate)
        {
            TapticPlugin.TapticManager.Impact(TapticPlugin.ImpactFeedback.Light);
        }
    }

    public void Vibrate()
    {
        if (canVibrate)
        {
            Handheld.Vibrate();
        }
    }

    public void ChangeVibration(bool b)
    {
        canVibrate = b;
    }
}
