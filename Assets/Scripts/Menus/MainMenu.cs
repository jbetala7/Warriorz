using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    public static bool gamePaused = false;
    public GameObject pauseMenu;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Button()
    {
        panel.GetComponent<Animator>().SetTrigger("pop");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

}
