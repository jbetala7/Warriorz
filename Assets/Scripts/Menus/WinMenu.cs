using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public static bool win = false;
    public GameObject winMenu;

    private void Awake()
    {
        winMenu.SetActive(false);
    }

    public void ReloadScene()
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

    public void Win()
    {
        winMenu.SetActive(true);
        Time.timeScale = 0f;
        win = true;
    }

}
