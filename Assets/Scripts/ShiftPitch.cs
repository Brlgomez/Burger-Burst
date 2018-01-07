using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftPitch : MonoBehaviour
{
    AudioSource source;
    bool changePitch;
    float pitchEnd;
    int direction;

    void Update()
    {
        if (changePitch)
        {
            source.pitch += Time.deltaTime * direction;
            if (source.pitch >= pitchEnd && direction > 0)
            {
                source.pitch = pitchEnd;
                Destroy(GetComponent<ShiftPitch>());
            }
            else if (source.pitch <= pitchEnd && direction < 0)
            {
                source.pitch = pitchEnd;
                Destroy(GetComponent<ShiftPitch>());
            }
        }
    }

    public void SetPitch(float newPitch)
    {
        source = GetComponent<AudioSource>();
        changePitch = true;
        pitchEnd = newPitch;
        if (source.pitch > newPitch)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
    }
}
