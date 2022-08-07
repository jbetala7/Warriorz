using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public static bool gameOver = false;
    public GameObject gameOverMenu;

    private void Awake()
    {
        gameOverMenu.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(2);
        AudioListener.volume = 1;

        Time.timeScale = 1;
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void PlayMusic()
    {
        AudioListener.volume = 1;
    }

    public void MuteMusic()
    {
        AudioListener.volume = 0;
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
        gameOver = true;
    }

}
