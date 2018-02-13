using UnityEngine;

public class CoinEffect : MonoBehaviour
{
    static int rotatingSpeed = 450;
    static float upwardSpeed = 2;
    static float lengthOfAnimation = 0.75f;
    static float lengthOfScaleUp = 0.25f;
    static float lengthOfScaleDown = 0.25f;

    float time;

    void Start()
    {
        transform.localScale = Vector3.zero;
        GetComponent<ParticleSystem>().Play();
        Camera.main.GetComponent<SoundAndMusicManager>().PlayFromSource(gameObject);
    }

    void Update()
    {
        time += Time.deltaTime;
        RaiseAndSpin();
        if (time < lengthOfScaleUp)
        {
            transform.localScale = Vector3.one * (time / lengthOfScaleUp);
        }
        else if (time > (lengthOfAnimation - lengthOfScaleDown))
        {
            transform.localScale = Vector3.one * ((lengthOfAnimation / lengthOfScaleDown) - (time / lengthOfScaleDown));
        }
        if (time > lengthOfAnimation)
        {
            Destroy(gameObject);
        }
    }

    void RaiseAndSpin()
    {
        float newY = transform.position.y + (upwardSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        transform.RotateAround(
            transform.transform.position,
            transform.up, rotatingSpeed * Time.deltaTime
        );
    }
}
