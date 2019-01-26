using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeZone : MonoBehaviour
{
    private string ENEMY_TAG = "Enemy";

    public Text roundOverText;
    private bool homeIsHit = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ENEMY_TAG))
        {
            homeIsHit = true;
            roundOverText.text = "Round Over";
        }
    }

    private void Update()
    {
        if (homeIsHit && Input.anyKeyDown)
        {
            roundOverText.text = "";
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
