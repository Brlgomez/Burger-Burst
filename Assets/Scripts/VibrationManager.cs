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

    public void Vibrate()
    {
        if (canVibrate)
        {
            Handheld.Vibrate();
        }
    }

    public void SelectTapticFeedback()
    {
        if (canVibrate)
        {
            TapticPlugin.TapticManager.Selection();
        }
    }

    public void LightTapticFeedback()
    {
        if (canVibrate)
        {
            TapticPlugin.TapticManager.Impact(TapticPlugin.ImpactFeedback.Light);
        }
    }

    public void MediumTapticFeedback()
    {
        if (canVibrate)
        {
            TapticPlugin.TapticManager.Impact(TapticPlugin.ImpactFeedback.Midium);
        }
    }

    public void HeavyTapticFeedback()
    {
        if (canVibrate)
        {
            TapticPlugin.TapticManager.Impact(TapticPlugin.ImpactFeedback.Heavy);
        }
    }

    public void SuccessTapticFeedback()
    {
        if (canVibrate)
        {
            TapticPlugin.TapticManager.Notification(TapticPlugin.NotificationFeedback.Success);
        }
    }

    public void WariningTapticFeedback()
    {
        if (canVibrate)
        {
            TapticPlugin.TapticManager.Notification(TapticPlugin.NotificationFeedback.Warning);
        }
    }

    public void ErrorTapticFeedback()
    {
        if (canVibrate)
        {
            TapticPlugin.TapticManager.Notification(TapticPlugin.NotificationFeedback.Error);
        }
    }

    public void ChangeVibration(bool b)
    {
        canVibrate = b;
    }
}
