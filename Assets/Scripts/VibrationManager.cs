using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    bool canVibrate = true;
    bool hapticFeedback = true;

    void Start()
    {
        canVibrate = GetComponent<PlayerPrefsManager>().GetVibration();
        hapticFeedback = GetComponent<PlayerPrefsManager>().GetHaptic();
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
        if (hapticFeedback && TapticPlugin.TapticManager.IsSupport())
        {
            TapticPlugin.TapticManager.Selection();
        }
    }

    public void LightTapticFeedback()
    {
        if (hapticFeedback && TapticPlugin.TapticManager.IsSupport())
        {
            TapticPlugin.TapticManager.Impact(TapticPlugin.ImpactFeedback.Light);
        }
    }

    public void MediumTapticFeedback()
    {
        if (hapticFeedback && TapticPlugin.TapticManager.IsSupport())
        {
            TapticPlugin.TapticManager.Impact(TapticPlugin.ImpactFeedback.Midium);
        }
    }

    public void HeavyTapticFeedback()
    {
        if (hapticFeedback && TapticPlugin.TapticManager.IsSupport())
        {
            TapticPlugin.TapticManager.Impact(TapticPlugin.ImpactFeedback.Heavy);
        }
    }

    public void SuccessTapticFeedback()
    {
        if (hapticFeedback && TapticPlugin.TapticManager.IsSupport())
        {
            TapticPlugin.TapticManager.Notification(TapticPlugin.NotificationFeedback.Success);
        }
    }

    public void WariningTapticFeedback()
    {
        if (hapticFeedback && TapticPlugin.TapticManager.IsSupport())
        {
            TapticPlugin.TapticManager.Notification(TapticPlugin.NotificationFeedback.Warning);
        }
    }

    public void ErrorTapticFeedback()
    {
        if (hapticFeedback && TapticPlugin.TapticManager.IsSupport())
        {
            TapticPlugin.TapticManager.Notification(TapticPlugin.NotificationFeedback.Error);
        }
    }

    public void ChangeVibration(bool b)
    {
        canVibrate = b;
    }

    public void ChangeHaptic(bool b)
    {
        hapticFeedback = b;
    }

    public bool HapticCompatible()
    {
        return TapticPlugin.TapticManager.IsSupport();
    }
}
