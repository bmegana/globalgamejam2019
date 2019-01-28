using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public static SoundPlayer instance;

    public AudioClip track1;
    public AudioClip track2;
    public AudioClip track3;
    public AudioSource source;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
        source = GetComponent<AudioSource>();
    }

    public void PlayTrackOne()
    {
        Debug.Log("Playing Track One.");
        source.clip = track1;
        source.Play();
    }

    public void PlayTrackTwo()
    {
        Debug.Log("Playing Track Two.");
        source.clip = track2;
        source.Play();
    }

    public void PlayTrackThree()
    {
        Debug.Log("Playing Track Three.");
        source.clip = track3;
        source.Play();
    }
}
