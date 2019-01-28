using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScene : MonoBehaviour
{
    double time = 0;
    private void Start()
    {
        SoundPlayer.instance.PlayTrackThree();
    }

    public void Update()
    {
        time += Time.deltaTime;
        if (time >= 62)
        {
            SceneManager.LoadScene("TitleCard");
        }
    }
}
