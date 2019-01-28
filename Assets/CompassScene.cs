using UnityEngine;
using UnityEngine.SceneManagement;

public class CompassScene : MonoBehaviour
{
    double time = 0;
    private void Start()
    {
        SoundPlayer.instance.PlayTrackTwo();
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= 25)
        {
            SceneManager.LoadScene("EndingScene");
        }
    }
}
