using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishClip;
    private bool levelCompleted = false;

    private void Awake()
    {
        finishClip = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !levelCompleted)
        {
            finishClip.Play();
            levelCompleted = true;
            Invoke("LevelComplete", 0.3f);
        }
    }

    private void LevelComplete()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
