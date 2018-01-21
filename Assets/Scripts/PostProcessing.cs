using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessing : MonoBehaviour
{
    //PostProcessVolume volume;
    //MotionBlur motionBlur;
    void Start()
    {
        /*
        motionBlur = ScriptableObject.CreateInstance<MotionBlur>();
        motionBlur.enabled.Override(false);
        motionBlur.shutterAngle.Override(180);
        motionBlur.sampleCount.Override(5);
        volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, motionBlur);
        */
    }

    public void PlayMotionBlur()
    {
        //motionBlur.enabled.Override(true);
    }

    public void StopMotionBlur()
    {
        //motionBlur.enabled.Override(false);
    }
}
