using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject[] tutorialPanel;

    private int tutorialCount = 0;

    void Start()
    {
        foreach (GameObject gb in tutorialPanel)
        {
            gb.SetActive(false);
        }
        tutorialPanel[0].SetActive(true);
        tutorialCount = 1;
        Time.timeScale = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1;

            foreach (GameObject gb in tutorialPanel)
            {
                gb.SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Tutorial")
        {
            other.gameObject.SetActive(false);
            tutorialPanel[tutorialCount].SetActive(true);
            tutorialCount++;
            Time.timeScale = 0;
        }
    }
}
