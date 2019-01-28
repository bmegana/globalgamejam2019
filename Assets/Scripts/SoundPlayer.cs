using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public static SoundPlayer instance;

    public AudioClip track1;
    public AudioClip track2;
    public AudioClip track3;
    private AudioSource source;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        source = GetComponent<AudioSource>();
    }

    public void PlayTrackOne()
    {
        source.clip = track1;
        source.Play();
    }

    public void PlayTrackTwo()
    {
        source.clip = track2;
        source.Play();
    }

    public void PlayTrackThree()
    {
        source.clip = track3;
        source.Play();
    }
}
