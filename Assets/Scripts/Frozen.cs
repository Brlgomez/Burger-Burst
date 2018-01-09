using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frozen : MonoBehaviour
{
    int updateInterval = 10;

    static int maxFrozenTime = 5;
    static float unFreezeTime = 0.25f;
    static float maxAplha = 0.5f;
    float time;
    float iceAlpha;
    Material screen;
    Color frozenColor;

    void Start()
    {
        frozenColor = new Color(1, 1, 1, maxAplha);
        screen = transform.GetChild(6).GetComponent<Renderer>().material;
        screen.color = frozenColor;
        GetComponent<GrabAndThrowObject>().SetFrozen(true);
        GetComponent<SoundAndMusicManager>().PlayFreezeSound(gameObject);
    }

    void Update()
    {
        if (Time.frameCount % updateInterval == 0)
        {
            time += Time.deltaTime * updateInterval;
            if (time > maxFrozenTime)
            {
                iceAlpha = (1 - ((time - maxFrozenTime) / unFreezeTime)) * maxAplha;
                screen.color = new Color(1, 1, 1, iceAlpha);
                updateInterval = 5;
            }
            if (time > maxFrozenTime + unFreezeTime)
            {
                DestroyFrozen();
            }
        }
    }

    public void RestartTime()
    {
        time = 0;
        updateInterval = 10;
        iceAlpha = maxAplha;
        screen.color = frozenColor;
        GetComponent<SoundAndMusicManager>().PlayFreezeSound(gameObject);
    }

    public void DestroyFrozen()
    {
        screen.color = Color.clear;
        GetComponent<GrabAndThrowObject>().SetFrozen(false);
        Destroy(GetComponent<Frozen>());
    }
}
