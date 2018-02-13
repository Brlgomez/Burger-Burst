using UnityEngine;

public class ZombieSounds : MonoBehaviour
{
    public void PlayWalkingSound()
    {
        Camera.main.GetComponent<SoundAndMusicManager>().PlayWalkingOnGrassSound(gameObject);
    }
}
