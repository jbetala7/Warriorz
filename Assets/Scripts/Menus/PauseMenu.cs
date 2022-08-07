using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject pauseMenu;
    private PlayerAttack playerAttack;

    private void Awake()
    {
        pauseMenu.SetActive(false);
        playerAttack = GameObject.Find("Player").GetComponent<PlayerAttack>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gamePaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AudioListener.volume = 1;

        Resume();
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

    public void Resume()
    {
        playerAttack.StartAttackDelay();
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
        playerAttack.canAttack = false;
    }

}
